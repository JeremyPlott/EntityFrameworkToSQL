using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkToSQL;
using System.Linq;

namespace EntityFrameworkToSQLLibrary {

    class RequestLineRepository {

        private static PRSdbContext context = new PRSdbContext();

        private static void RecalcReqTotal(int requestId) {
            var request = RequestRepository.GetByPK(requestId);
            request.Total = request.RequestLines.Sum(l => l.Product.Price * l.Quantity);
            // save changes is after this code in Insert, Update, Delete
        }

        public static List<RequestLines> GetAll() {
            return context.RequestLines.ToList();
        }
        public static RequestLines GetByPK(int id) {
            if (id < 1) { throw new Exception("RequestLine Id must be greater than 0"); }
            return context.RequestLines.Find(id);
        }
        public static bool Insert(RequestLines requestline) {
            if (requestline == null) { throw new Exception("RequestLine instance must not be null"); }
            requestline.Id = 0;
            context.RequestLines.Add(requestline);
            RecalcReqTotal(requestline.Id);
            return context.SaveChanges() == 1;
        }
        public static bool Update(RequestLines requestline) {
            if (requestline == null) { throw new Exception("RequestLine not found"); }
            var dbrequestline = context.RequestLines.Find(requestline.Id);
            if (dbrequestline == null) { throw new Exception("RequestLine not found"); }
            dbrequestline.Id = requestline.Id;
            dbrequestline.RequestId = requestline.RequestId;
            dbrequestline.ProductId = requestline.ProductId;
            dbrequestline.Quantity = requestline.Quantity;
            RecalcReqTotal(requestline.Id);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(RequestLines requestline) {
            if (requestline == null) { throw new Exception("RequestLine not found"); }
            var dbrequestline = context.RequestLines.Find(requestline.Id);
            if (dbrequestline == null) { throw new Exception("RequestLine not found"); }
            context.RequestLines.Remove(dbrequestline);
            RecalcReqTotal(requestline.Id);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var requestline = context.RequestLines.Find(id);
            if (requestline == null) { return false; }
            var rc = Delete(requestline);
            return rc;
        }
    }
}
