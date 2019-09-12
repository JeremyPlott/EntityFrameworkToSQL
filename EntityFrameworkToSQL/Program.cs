using System;
using System.Linq;

namespace EntityFrameworkToSQL {

    class Program {

        static void Main(string[] args) {

            using (var context = new PRSdbContext()) { // using statement isn't necessary, but it will automatically dispose of this once it's finished. Efficiency.

                //var request = new Requests() {
                //    Id = 0,
                //    Description = "Another new request",
                //    Justification = "Moreee data in the table",
                //    RejectionReason = null,
                //    DeliveryMode = "Pickup",
                //    Status = "NEW",
                //    Total = 0,
                //    UserId = context.Users.SingleOrDefault(u => u.Username.Equals("NotAGiraffe")).Id
                //};
                //context.Requests.Add(request);

                //var request = new Requests() { Id = 3, Description = "Changed again" };
                //var dbRequest = context.Requests.Find(request.Id);
                //dbRequest.Description = request.Description;

                ////dbRequest = context.Requests.Find(3);
                ////context.Requests.Remove(dbRequest);
                //context.SaveChanges();

                //request, status, total, then line items for name qty price


                var req3 = context.Requests.Find(3);

                req3.Total = req3.RequestLines.Sum(l => l.Product.Price * l.Quantity);
                context.SaveChanges();

                Console.WriteLine($"{req3.Description}, {req3.Status}, {req3.Total.ToString("C")}");
                req3.RequestLines.ToList().ForEach(rl => {
                    Console.WriteLine($"{rl.Product.Name,-10} {rl.Quantity,5} " +
                        $"{rl.Product.Price.ToString("C"),10} " +
                        $"{(rl.Product.Price * rl.Quantity).ToString("C"),11}");
                });
                Console.WriteLine("");

                var allreq = (from r in context.Requests.ToList()
                              select r.Total).Sum();

                //var total = context.Requests.Sum(r => r.Total); // alternative with lambda syntax for above line

                Console.WriteLine("~~~~");
                Console.WriteLine($"Sum of request cost: {allreq.ToString("C")}");
                Console.WriteLine("~~~~");



                var vendors = context.Vendors.ToList(); // run a quick-watch on this and it shows assoc products as well

                vendors.ForEach(v => Console.WriteLine($"{v.Code} {v.Name}"));
                Console.WriteLine();


                var vendorsPK = context.Vendors.Find(1); // searches PK value in cached data first before going to DB, returns null if not found
                Console.WriteLine($"{vendorsPK.Code} {vendorsPK.Name}");
                Console.WriteLine();



                //var users = context.Users.ToList();
                var users = from u in context.Users.ToList()
                            where u.Username.Contains("r") || u.Username.Contains("n")
                            select u;

                foreach(var user in users) {
                    Console.WriteLine($"{user.Username} is {user.Firstname} {user.Lastname}");
                }
            }
        }
    }
}
