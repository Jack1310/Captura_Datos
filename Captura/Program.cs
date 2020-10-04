using System;
using System.Text;
using System.IO;

namespace Captura
{
    class Program
    {
        public static HashSetArrayBased hashSet = new HashSetArrayBased();

        static void Main(string[] args)
        {
            int dato;

            if (args[0] == "-R")
            {
                var lines = File.ReadAllLines("Datos.csv");

                for (var i = 1; i < lines.Length; i += 1)
                {
                    try
                    {
                        var line = lines[i];
                        string[] datosperson = new string[6];
                        datosperson = line.Split(",");
                        dato = int.Parse(datosperson[4]);
                        Console.WriteLine("> Nombre completo: " + datosperson[0] + " " + datosperson[1]);
                        Console.WriteLine("> Edad: " + datosperson[2]);
                        Console.WriteLine("> Ahorros: " + datosperson[3]);
                        Console.WriteLine("> Password: " + datosperson[5]);
                        Descodificacion(dato);
                        Console.WriteLine("\n");
                    }
                    catch{}
                }
            }

            else if(args[0] == "-S")
            {
                
                if(!File.Exists("Datos.csv"))
                {
                    StreamWriter Archivo = new StreamWriter("Datos.csv");
                    Archivo.Write("Nombre, Apellidos, Edad, Ahorros, No. Info, Password");
                    Archivo.Close();
                }
                
                var lines = File.ReadAllLines("Datos.csv");
                try{
                for (int i = 1; i < lines.Length; i++)
                {
                    var line = lines[i];
                    string[] datosperson = new string[6];
                    datosperson = line.Split(",");
                    Student persona = new Student(datosperson[0], datosperson[1], datosperson[2], datosperson[3], datosperson[5], datosperson[4]);
                    hashSet.Add(persona);
                }}
                catch{}
                
                
                Console.WriteLine("1) Agregar\n2) Editar\n3) Eliminar\n");
                int menu = int.Parse(Console.ReadLine());

                switch (menu)
                {
                    case 1:
                        Console.Clear();
                        Agregar();
                    break;
                    
                    case 2:
                        Console.Clear();
                        Editar();
                    break;

                    case 3:
                        Console.Clear();
                        Eliminar();
                    break;
                }
            }
        }

        public static void Eliminar()
        {
            Console.Write("> Nombre: ");
            string a = Nombre();
            Console.Write("> Apellidos: ");
            string b = Nombre();
                
            Student persona = new Student(a, b);
            if (hashSet.Contains(persona))
            {
                hashSet.Remove(persona);
                File.WriteAllText("temp.csv", "Nombre, Apellido, Edad, Ahorro, Datos, Password" + Environment.NewLine);

                for (int i = 0; i < HashSetArrayBased.Buckets.Length; i++)
                {
                    foreach (var stud in HashSetArrayBased.Buckets[i])
                    {
                        File.AppendAllText("temp.csv", $"{stud.Nombre},{stud.Apellido},{stud.Edad},{stud.Ahorro},{stud.Dato},{stud.Password}" + Environment.NewLine);
                    }
                }

                File.Delete("Datos.csv");
                File.Move("temp.csv", "Datos.csv");               
                Console.WriteLine("Se ha eliminado con éxito");
            }
            else
            {
                Console.WriteLine("La persona no se ha eliminado, debido a que no se encuentra guardado en el registro.");
            }            
        }
        

        public static void Agregar()
        {
            Console.Write("> Nombre: ");
            string a = Nombre();
            Console.Write("> Apellidos: ");
            string b = Nombre();

            Student persona = new Student(a, b);
            if (hashSet.Contains(persona))
            {
                Console.WriteLine("No se puede agregar porque ya existe");
            }
            
            else
            {
                string c = Edad(), d = Ahorro();
                Console.Write("> Password: ");
                string e = Password();
                Console.Write("> Confirmar Password: ");
                string f = Password();
                int z = Info(), y = int.Parse(c);
                    if (y > 17)
                    {
                        z = z | 2;
                    }

                Console.Write("\n> Tu nombre es: " + a);
                Console.Write("\n> Tus apellidos son: " + b);
                Console.Write("\n> Tu edad es: " + c);
                Console.Write("\n> Tu ahorros son: $" + d);
                Console.Write("\n> Passwords match? " + e.Equals(f));
                Console.ReadLine();

                if(e.Equals(f))
                {
                    StreamWriter Archivo = new StreamWriter("Datos.csv" , true);
                    Archivo.Write("{0},{1},{2},{3},{5},{4}" , a, b, c, d, e, z);
                    Archivo.Close();
                }
            }
        }

        public static void Editar()
        {
            Console.Write("> Nombre: ");
            string a = Nombre();
            Console.Write("> Apellidos: ");
            string b = Nombre();
                
            Student persona = new Student(a, b);
            if (hashSet.Contains(persona))
            {
                string c = Edad(), d = Ahorro();
                Console.Write("> Password: ");
                string e = Password();
                Console.Write("> Confirmar Password: ");
                string f = Password();
                int z = Info(), y = int.Parse(c);
                    if (y > 17)
                    {
                        z = z | 2;
                    }

                Console.Write("\n> Tu nombre es: " + a);
                Console.Write("\n> Tus apellidos son: " + b);
                Console.Write("\n> Tu edad es: " + c);
                Console.Write("\n> Tu ahorros son: $" + d);
                Console.Write("\n> Passwords match? " + e.Equals(f));
                Console.ReadLine();

                string nombrefull = a + "," + b;
                var lines = File.ReadAllLines("Datos.csv");
                    
                for (var i = 1; i < lines.Length; i += 1)
                {
                    var line = lines[i];
                        //DoesLineExist(line);
                    if (!string.IsNullOrEmpty(line) && line.Contains(nombrefull))
                    {
                        string text = File.ReadAllText("Datos.csv");
                        text = text.Replace(line, nombrefull + "," + c + "," + d + "," + z + "," + e);
                        File.WriteAllText("Datos.csv", text);
                    }
                }
            }

            else
            {
                Console.WriteLine("La persona no se encuentra");
            }
        }

        public static int Descodificacion(int dato)
        {
            if (((dato ^ 1) % 2) == 0)
                Console.WriteLine("> Eres hombre");
            else
                Console.WriteLine("> Eres mujer");
            if ((((dato >> 1) ^ 1) % 2) == 0)
                Console.WriteLine("> Eres mayor de edad");
            else
                Console.WriteLine("> Eres menor de edad");
            if ((((dato >> 2) ^ 1) % 2) == 0)
                Console.WriteLine("> Tienes licencia");
            else
                Console.WriteLine("> No tienes licencia");
            if ((((dato >> 3) ^ 1) % 2) == 0)
                Console.WriteLine("> Tienes vehículo");
            else
                Console.WriteLine("> No tienes vehículo");            
            
            return dato;
        }
        
        public static int Info()
        {
            int x = 0;
            string Sexo, Lic, Vehi;
            
            Console.Write("> Sexo (m/f): ");
            Sexo = Console.ReadLine();
            if (Sexo == "m")
            {
                x = x | 1;
            }
                        
            Console.Write("> Licencia (y/n): ");
            Lic = Console.ReadLine();
            if (Lic == "y")
            {
                x = x | 4;
            }
            
            Console.Write("> Vehiculo (y/n): ");
            Vehi = Console.ReadLine();
            if (Vehi == "y")
            {
                x = x | 8;
            }

            return x;
        }

        public static string Nombre()
        {
            StringBuilder vcompleto = new StringBuilder();
            ConsoleKeyInfo vnombre;

            do
            {
                vnombre = Console.ReadKey();

                if (vnombre.KeyChar == 8 && vcompleto.Length > 0)
                {
                    vcompleto.Length--;
                    Console.Write(" \b");
                }

                if ((vnombre.KeyChar > 64 && vnombre.KeyChar < 91) || 
                (vnombre.KeyChar > 96 && vnombre.KeyChar < 123) || 
                vnombre.KeyChar == 32 || vnombre.KeyChar == 241 || vnombre.KeyChar == 209)
                {
                    try
                    {
                        vcompleto.Append(vnombre.KeyChar);
                    }
                    catch{}
                }
            } while (vnombre.KeyChar != 13);

            Console.WriteLine();
            return vcompleto.ToString();
        }

        public static string Edad()
        {
            StringBuilder vcompleto = new StringBuilder();
            ConsoleKeyInfo vnombre;

            Console.Write("> Edad: ");

            do
            {
                vnombre = Console.ReadKey();

                if (vnombre.KeyChar == 8 && vcompleto.Length > 0)
                {
                    vcompleto.Length--;
                    Console.Write(" \b");
                }

                if ((vnombre.KeyChar > 47 && vnombre.KeyChar < 58))
                {
                    try
                    {
                        vcompleto.Append(vnombre.KeyChar);
                    }
                    catch{}
                }
            } while (vnombre.KeyChar != 13);

            Console.WriteLine();
            return vcompleto.ToString();
        }

        public static string Ahorro()
        {
            StringBuilder vcompleto = new StringBuilder();
            ConsoleKeyInfo vnombre;

            Console.Write("> Ahorros: $");

            do
            {
                vnombre = Console.ReadKey();

                if (vnombre.KeyChar == 8 && vcompleto.Length > 0)
                {
                    vcompleto.Length--;
                    Console.Write(" \b");
                }

                if ((vnombre.KeyChar > 47 && vnombre.KeyChar < 58) ||
                (vnombre.KeyChar == 46) || vnombre.KeyChar == 44)
                {
                    try
                    {
                        vcompleto.Append(vnombre.KeyChar);
                    }
                    catch{}
                }
            } while (vnombre.KeyChar != 13);

            Console.WriteLine();
            return vcompleto.ToString();
        }

        public static string Password()
        {
            StringBuilder vcompleto = new StringBuilder();
            ConsoleKeyInfo vnombre;

            do
            {
                vnombre = Console.ReadKey(true);

                if (vnombre.KeyChar == 8 && vcompleto.Length > 0)
                {
                    vcompleto.Length--;
                    Console.Write("\b \b");
                }

                else if (vnombre.KeyChar != 13 && vnombre.KeyChar != 8)
                {
                    try
                    {
                        vcompleto.Append(vnombre.KeyChar);
                        Console.Write("*");
                    }
                    catch{}
                }
            } while (vnombre.KeyChar != 13);

            Console.WriteLine();
            return vcompleto.ToString();
        }
    }
}
