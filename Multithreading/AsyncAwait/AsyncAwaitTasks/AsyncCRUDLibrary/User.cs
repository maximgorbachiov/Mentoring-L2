using System;

namespace AsyncCRUDLibrary
{
    public class User : ICloneable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Name} {Surname} {Age}";
        }
    }
}
