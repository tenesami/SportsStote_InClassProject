using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSC237_tatomsa_InClassProject.DataLayer
{
    public class QueryOptions<T>
    {
        //public  Properties for sorting filtering and paging
        public Expression<Func<T, Object>> OrderBy { get; set; }
        public string OrderByDirection { get; set; } = "asc"; //Default

        //filter by more than ome where clause. User can set value for where property
        // repeatedly, or can instantiates a new WhereClauses property, whose type
        //is a class that inherits a list of where expressions (see below)

        public WhereClauses<T> WhereClauses { get; set; }   
        public Expression<Func<T, bool>> Where
        {
            set
            {
                if(WhereClauses == null)
                {
                    WhereClauses = new WhereClauses<T>();
                }
                WhereClauses.Add(value);
            }
        }

        //Private backing field for property and method that work with Include string
        private string[] includes;

        //public write-only property for Include string - converts comma-sepated string to array
        //and stores in private backing field
        public string Includes
        {
            set => includes = value.Replace(" ", "").Split(',');
        }

        //public get method for Include strings - returns private backing field or
        //empty string array if private backing field is null
        public string[] GetIncludes() => includes ?? new string[0];

        //read-only properties
        public bool HasWhere => WhereClauses != null;
        public bool HasOrderBy => OrderBy != null;
    }
    public class WhereClauses<T> : List<Expression<Func<T, bool>>> { }
}
