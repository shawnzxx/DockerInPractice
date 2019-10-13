using System;

namespace Audit.Domain.Models
{
    //Mapping to Private Properties with Code First
    //https://romiller.com/2012/10/01/mapping-to-private-properties-with-code-first/

    //how to use IEntityTypeConfiguration<T> 
    //https://codeburst.io/ientitytypeconfiguration-t-in-entityframework-core-3fe7abc5ee7a
    public class RunningTotal
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public DateTime UpdatedDate { get; set; }


        //for infra entity
        private RunningTotal() { }

        //for front end pass in data
        public RunningTotal(double previousTotal, double number)
        {
            Total = previousTotal + number;
            UpdatedDate = DateTime.Now;
        }

        public double GetRunningTotal()
        {
            return Total;
        }
    }
}
