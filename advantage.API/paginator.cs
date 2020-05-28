using System.Collections.Generic;
using System.Linq;

namespace advantage.API
{

    public class paginatedResponse<T>
    {

        public int Total{get;set;}
        public IEnumerable<T> Data{get;set;}
        
        public paginatedResponse( IEnumerable<T> data, int i, int len)
        {
            Data = data.Skip( (i-1)*len ).Take(len).ToList();
            Total = data.Count();
        }




    }

}