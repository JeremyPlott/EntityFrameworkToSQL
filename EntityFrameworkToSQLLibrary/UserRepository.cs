using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkToSQL;
using System.Linq;

namespace EntityFrameworkToSQLLibrary {

    class UserRepository {

        private static PRSdbContext context = new PRSdbContext();

        public static List<Users> GetAll() {
            return context.Users.ToList();
        }
        public static Users GetByPK(int id) {
            if (id < 1) { throw new Exception("User Id must be greater than 0"); }
            return context.Users.Find(id);
        }
        public static bool Insert(Users user) {
            if (user == null) { throw new Exception("User instance must not be null"); }
            user.Id = 0;
            context.Users.Add(user);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Users user) {
            if(user == null) { throw new Exception("User not found"); }
            var dbuser = context.Users.Find(user.Id);
            if (dbuser == null) { throw new Exception("User not found"); }
            dbuser.Id = user.Id;            
            dbuser.Username = user.Username;
            dbuser.Password = user.Password;
            dbuser.Firstname = user.Firstname;
            dbuser.Lastname = user.Lastname;
            dbuser.Phone = user.Phone;
            dbuser.Email = user.Email;
            dbuser.IsAdmin = user.IsAdmin;
            dbuser.IsReviewer = user.IsReviewer;
            dbuser.Requests = user.Requests;
            return context.SaveChanges() == 1;            
        }
        public static bool Delete(Users user) {
            if (user == null) { throw new Exception("User not found"); }
            var dbuser = context.Users.Find(user.Id);
            if (dbuser == null) { throw new Exception("User not found"); }
            context.Users.Remove(dbuser);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var user = context.Users.Find(id);
            if(user == null) { return false; }
            var rc = Delete(user);
            return rc;
        }
    }
}
