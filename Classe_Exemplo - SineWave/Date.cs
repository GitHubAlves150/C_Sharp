using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classe_Exemplo
{
    class Date
    {
        private short month, day;
        private int year;

        //construtor
        public Date(short argMonth, short argDay, int argYear)
        {
            if (argMonth > 0 && argMonth <= 12)
            {
                month = argMonth;
            }
            else
            {
                month = 1;
                Console.WriteLine(" {0} is invalid. Month set by 1.", argMonth);
            }
            day = VerifyDay(argDay);
            year= (argYear > 0 && argYear <= 999999)? argYear :2000;
        }//end Date;

        private short VerifyDay(short argDay)
        {
            short[] day = { 0, 31, 28, 31, 30, 31, 30, 30, 31, 30, 31, 30, 31};
            if(argDay >0 && argDay <= day[month])
            {
                return argDay;
            }
            if(month == 2 && argDay == 29 && year % 400 == 0 || year % 4==0 && year % 100 != 0 )                
            {
                return argDay;
            }
            Console.WriteLine("{0} is invalid. Day set by 1.", argDay);
            return 1;
        }//end verify Day

        public string ShowDate()
        {
            return year + "/" + month + "/" + day;
        }//end ShowDate


    }
}
