using System;
using System.Linq;

namespace EntityFrameworkToSQL {

    class Program {

        static void Main(string[] args) {

            using (var context = new PRSdbContext()) { // using statement isn't necessary, but it will automatically dispose of this once it's finished. Efficiency.

                var vendors = context.Vendors.ToList(); // run a quick-watch on this and it shows assoc products as well

                vendors.ForEach(v => Console.WriteLine($"{v.Code} {v.Name}"));
                Console.WriteLine();


                var vendorsPK = context.Vendors.Find(1); // searches PK value in cached data first before going to DB, returns null if not found
                Console.WriteLine($"{vendorsPK.Code} {vendorsPK.Name}");
                Console.WriteLine();



                var users = context.Users.ToList();

                foreach(var user in users) {
                    Console.WriteLine($"{user.Username} is {user.Firstname} {user.Lastname}");
                }
            }
        }
    }
}
