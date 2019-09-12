using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkToSQL;
using System.Linq;

namespace EntityFrameworkToSQLLibrary {

    class ProductRepository {

        private static PRSdbContext context = new PRSdbContext();

        public static List<Products> GetAll() {
            return context.Products.ToList();
        }
        public static Products GetByPK(int id) {
            if (id < 1) { throw new Exception("Product Id must be greater than 0"); }
            return context.Products.Find(id);
        }
        public static bool Insert(Products product) {
            if (product == null) { throw new Exception("Product instance must not be null"); }
            product.Id = 0;
            context.Products.Add(product);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Products product) {
            if (product == null) { throw new Exception("Product not found"); }
            var dbproduct = context.Products.Find(product.Id);
            if (dbproduct == null) { throw new Exception("Product not found"); }
            dbproduct.Id = product.Id;
            dbproduct.PartNbr = product.PartNbr;
            dbproduct.Name = product.Name;
            dbproduct.Price = product.Price;
            dbproduct.Unit = product.Unit;
            dbproduct.PhotoPath = product.PhotoPath;
            dbproduct.VendorId = product.VendorId;
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Products product) {
            if (product == null) { throw new Exception("Product not found"); }
            var dbproduct = context.Products.Find(product.Id);
            if (dbproduct == null) { throw new Exception("Product not found"); }
            context.Products.Remove(dbproduct);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var product = context.Products.Find(id);
            if (product == null) { return false; }
            var rc = Delete(product);
            return rc;
        }
    }
}
