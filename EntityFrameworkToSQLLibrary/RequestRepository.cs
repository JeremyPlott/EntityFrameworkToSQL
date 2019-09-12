using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkToSQL;
using System.Linq;

namespace EntityFrameworkToSQLLibrary {

    class RequestRepository {

        private static PRSdbContext context = new PRSdbContext();

        public static List<Requests> GetAll() {
            return context.Requests.ToList();
        }
        public static Requests GetByPK(int id) {
            if (id < 1) { throw new Exception("Request Id must be greater than 0"); }
            return context.Requests.Find(id);
        }
        public static bool Insert(Requests request) {
            if (request == null) { throw new Exception("Request instance must not be null"); }
            request.Id = 0;
            context.Requests.Add(request);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Requests request) {
            if (request == null) { throw new Exception("Request not found"); }
            var dbrequest = context.Requests.Find(request.Id);
            if (dbrequest == null) { throw new Exception("Request not found"); }
            dbrequest.Id = request.Id;
            dbrequest.Description = request.Description;
            dbrequest.Justification = request.Justification;
            dbrequest.RejectionReason = request.RejectionReason;
            dbrequest.DeliveryMode = request.DeliveryMode;
            dbrequest.Status = request.Status;
            dbrequest.Total = request.Total;
            dbrequest.UserId = request.UserId;
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Requests request) {
            if (request == null) { throw new Exception("Request not found"); }
            var dbrequest = context.Requests.Find(request.Id);
            if (dbrequest == null) { throw new Exception("Request not found"); }
            context.Requests.Remove(dbrequest);
            return context.SaveChanges() == 1;
        }
        public static bool Insert(int id) {
            var request = context.Requests.Find(id);
            if (request == null) { return false; }
            var rc = Delete(request);
            return rc;
        }
    }
}
