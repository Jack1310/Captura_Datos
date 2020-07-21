using System;
using System.Diagnostics.CodeAnalysis;

namespace Captura
{
    public sealed class Student
    {
        public string Nombre { get; }
        public string Apellido { get; }
        public string Edad { get; }
        public string Ahorro { get; }
        public string Password { get; }
        public string Dato { get; }

        public Student(string a, string b, string c, string d, string e, string z)
            => (Nombre, Apellido, Edad, Ahorro, Password, Dato) = (a, b, c, d, e, z);
        public Student(string a, string b)
             => (Nombre, Apellido) = (a, b);
        
        public override bool Equals(object obj)
        {
            Student persona = obj as Student;
            if (persona == null) { return false; }
            string persona1 = $"{this.Nombre} {this.Apellido}";
            string persona2 = $"{persona.Nombre} {persona.Apellido}";
            return persona1.Equals(persona2,StringComparison.OrdinalIgnoreCase);
        }
        
        public override int GetHashCode()
        {
            char InicialApellido = Apellido[0];

            return char.ToLowerInvariant(InicialApellido);
        }
    }
}