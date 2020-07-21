using System;
using System.Collections.Generic;
using System.Text;

namespace Captura
{
    class HashSetArrayBased : HashSet<Student>
    {
        public static LinkedList<Student>[] Buckets =
        {
            new LinkedList<Student>(),
            new LinkedList<Student>(),
            new LinkedList<Student>(),
            new LinkedList<Student>(),
            new LinkedList<Student>(),
            new LinkedList<Student>(),
        };

        public new bool Add(Student persona)
        {
            if (!Contains(persona))
            {
                var bucket = Buckets[persona.GetHashCode() % 6];
                bucket.AddLast(persona);
                return true;
            }
            return false;
        }

        public new bool Contains(Student persona)
        {
            var bucket = Buckets[persona.GetHashCode() % 6];
            foreach (var per in bucket)
            {
                if (per.Equals(persona))
                {
                    return true;
                }    
            }
            return false;
        }
        
        public new bool Remove(Student persona)
        {
            if (Contains(persona))
            {
                int hashCodePersona = persona.GetHashCode() % 6; 
                foreach (var per in Buckets[hashCodePersona])
                {
                    if (per.Equals(persona))
                    {
                        Buckets[hashCodePersona].Remove(persona);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}