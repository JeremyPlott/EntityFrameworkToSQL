using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkToSQL;
using System.Linq;

namespace EntityFrameworkToSQLLibrary {

    class VendorRepository {

        private static PRSdbContext context = new PRSdbContext();

        public static List<Vendors> GetAll() {
            return context.Vendors.ToList();
        }
        public static Vendors GetByPK(int id) {
            if (id < 1) { throw new Exception("Vendor Id must be greater than 0"); }
            return context.Vendors.Find(id);
        }
        public static bool Insert(Vendors vendor) {
            if (vendor == null) { throw new Exception("Vendor instance must not be null"); }
            vendor.Id = 0;
            context.Vendors.Add(vendor);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Vendors vendor) {
            if (vendor == null) { throw new Exception("Vendor not found"); }
            var dbvendor = context.Vendors.Find(vendor.Id);
            if (dbvendor == null) { throw new Exception("Vendor not found"); }
            dbvendor.Id = vendor.Id;
            dbvendor.Name = vendor.Name;
            dbvendor.Code = vendor.Code;
            dbvendor.Products = vendor.Products;
            dbvendor.Address = vendor.Address;
            dbvendor.Zip = vendor.Zip;
            dbvendor.City = vendor.City;
            dbvendor.State = vendor.State;
            dbvendor.Email = vendor.Email;
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Vendors vendor) {
            if (vendor == null) { throw new Exception("Vendor not found"); }
            var dbvendor = context.Vendors.Find(vendor.Id);
            if (dbvendor == null) { throw new Exception("Vendor not found"); }
            context.Vendors.Remove(dbvendor);
            return context.SaveChanges() == 1;
        }
        public static bool Insert(int id) {
            var vendor = context.Vendors.Find(id);
            if (vendor == null) { return false; }
            var rc = Delete(vendor);
            return rc;
        }
    }
}
