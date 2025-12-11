using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GemeloDigital
{
    internal partial class Storage02 : Storage
    {
        internal override void SaveScene(string storageId)
        {
            if (!File.Exists("saves/" + storageId+".sb"))
            {
                Console.WriteLine("No existe la escena, creandola...");
                Thread.Sleep(1000);
            }

            FileStream ficha = new FileStream("saves/"+storageId+".sb", FileMode.Create,FileAccess.Write);

            //puntos
                //sacar puntos
                List<SimulatedObject> points = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Point);
                //cantidad de puntos
                    bytes = new byte[sizeof(int)];
                    bytes = BitConverter.GetBytes(points.Count);
                    ficha.Write(bytes);
                //puntos
                foreach (SimulatedObject point in points)
                {
                    Point p = SimulatorCore.AsPoint(point);

                    //id
                    bytes = Guid.Parse(p.Id).ToByteArray(); //convertir guid a bytes
                    ficha.Write(bytes);
                    
                    //grandaria nombre
                    bytes = new byte[sizeof(int)];
                    bytes = BitConverter.GetBytes(p.Name.Length);
                    ficha.Write(bytes);
                    //nombre
                    bytes = System.Text.Encoding.UTF8.GetBytes(p.Name);
                    ficha.Write(bytes);

                    //x
                    bytes = BitConverter.GetBytes(p.Position.X);
                    ficha.Write(bytes);
                    //y
                    bytes = BitConverter.GetBytes(p.Position.Y);
                    ficha.Write(bytes);
                    //z
                    bytes = BitConverter.GetBytes(p.Position.Z);
                    
                    Console.WriteLine($@"punto id = {p.Id},
                                      name = {p.Name}, x = {p.Position.X},
                                      y = {p.Position.Y}, z= {p.Position.Z}");
                 
                }


            //paths
                //sacar paths
                List<SimulatedObject> paths = SimulatorCore.FindObjectsOfType(SimulatedObjectType.Path);
                //cantidad de paths
                bytes = new byte[sizeof(int)];
                bytes = BitConverter.GetBytes(paths.Count);
                ficha.Write(bytes);
                //paths
                foreach (SimulatedObject path in paths)
                {
                    Point p = SimulatorCore.AsPoint(path);

                    //id
                    bytes = Guid.Parse(p.Id).ToByteArray(); //convertir guid a bytes
                    ficha.Write(bytes);

                    //grandaria nombre
                    bytes = new byte[sizeof(int)];
                    bytes = BitConverter.GetBytes(p.Name.Length);
                    ficha.Write(bytes);
                    //nombre
                    bytes = System.Text.Encoding.UTF8.GetBytes(p.Name);
                    ficha.Write(bytes);

                    
                
                }



            //guardar ficha
            ficha.Close();
            lista_storages.Add(storageId);//meter ficha a la lista
        }
    }
}
