using System;
using System.Collections.Generic;
using System.Text;

namespace Compute.Domain.Models
{
    public class Operation
    {
        public int Id { get; internal set; }
        public double X { get; internal set; }
        public double Y { get; internal set; }
        public double Result { get; internal set; }
        public OperationTypeEnum OperationType { get; internal set; }

        //https://gunnarpeipman.com/data/constructors-with-arguments-ef-core/
        //Constructor for EF core: https://docs.microsoft.com/en-us/ef/core/modeling/constructors
        //The parameter types and names must match property types and names
        private Operation(double x, double y, OperationTypeEnum operationType)
        {
            X = x;
            Y = y;
            OperationType = operationType;
            Result = CalculateResult();
        }
        //Constructor for developer pass in data
        public Operation(double x, double y, int optInt) : this(x, y, (OperationTypeEnum)optInt)
        {

        }

        private double CalculateResult()
        {
            switch (OperationType)
            {
                case OperationTypeEnum.Add:
                    return X + Y;
                case OperationTypeEnum.Sub:
                    return X - Y;
                case OperationTypeEnum.Mul:
                    return X * Y;
                case OperationTypeEnum.Div:
                    return X / Y;
                default:
                    return X + Y;
            }
        }

        //Type of the operation
        public enum OperationTypeEnum
        {
            Add = 1,
            Sub = 2,
            Mul = 3,
            Div = 4
        }
    }
}
