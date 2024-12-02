using Paksalaszlo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paksalaszlo
{

    internal class Author
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Guid Id { get; private set; }

        Author(string fullName)
        {
            var names = fullName.Split(' ');
            if (names.Length != 2 || names[0].Length < 3 || names[1].Length < 3 || names[0].Length > 32 || names[1].Length > 32)
                throw new ArgumentException("A név nem megfelelő!");

            FirstName = names[0];
            LastName = names[1];
            Id = Guid.NewGuid();
        }
    }
}

