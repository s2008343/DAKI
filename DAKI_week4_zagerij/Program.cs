using System;
using System.Linq;
using System.Collections.Generic;


namespace zagerij
{
   /**
    * Zaag in vierkante stukken a en b
    * invoer h hoogte  b breedte k sorting switch threshold 
    * (h-1) rijen invoer kosten verticale zaagbewegingen
    * (b-1) rijen invoer kosten horizontale zaagbeweging
    *
    * Divide into large as squares as possible? least amount of cuts to minimize cost
    * cost of cut 
    * 
    * 
    */
   public class Zaag : IComparable<Zaag>
   {
      public int prijs { get; set; }
      public bool horizontaal { get; set; }
   

      public Zaag(int prijs, bool horizontaal)
      {
         this.prijs = prijs;
         this.horizontaal = horizontaal;
         
      }
      
      
      public int CompareTo(Zaag other)
      {
         // change greater or smaller the operator for ascending descending
         if (this.prijs < other.prijs)
         {
            return -1;
         }
         else if(this.prijs > other.prijs)
         {
            return 1;
         }

         return 0;
      }

 
      
   }

   

   public class Zagerij
   {
    
      
      
      public static void selectionSort<T>(List<T> A,int low,int high) where T:IComparable<T>
      {

         int min;
         for (int i = low; i < high-1; i++)
         {

            min = i;
            for (int j = i + 1; j <= high; j++)
            {
               T temp1 = A[j];
               T temp2 = A[min];

               bool test = temp1.CompareTo(temp2) == 1;
               if (test)
               {
                  min = j;
               }
            }

           
            T temp = A[min];
            
            A[min] = A[i];
            A[i] = temp;
            
           //A = swap(A, min, i);

         }

         
      }

      public static void  quickSort<T>(List<T> A,int low,int high,int k) where T:IComparable<T>
      {
         
         //perform select sort if length of list is shorter then threshold k 
         int lengthOfA = Math.Abs(high-low)+1;
         if (lengthOfA<=k)
         {
            selectionSort(A,low,high);

         }
         else
         {
            if (low < high)
            {
               /*
               if (lengthOfA <= k)
               {
                  selectionSort(A,low,high);
               }
               else
               */
               {
                  //partition the list 
                  int pi = partition(A, low, high);
                  quickSort(A, low, pi - 1, k);
                  quickSort(A, pi + 1, high, k);
               }
            }
         }
      }
      
      private static int partition<T>(List<T> A, int low, int high) where T:IComparable<T>
      {
         T pivot = A[high];
         int i = low;

         
         //iff item A[j] is greater then the pivot put it to the left of the pivot,
         //items smaller then the pivot are put into the right array
         //smaller and greater than are opposite of the normal implemented quicksort array to inverse the order of the array to make it ascending
         for (int j = low; j < high; j++)
         {
            T itemTemp = A[j];
            if (itemTemp.CompareTo(pivot)==1) // item.prijs > pivot.prijs
            {
               A[j] = A[i];
               A[i] = itemTemp;
               i++;
              
            }
         }

         A[high] = A[i];
         A[i] = pivot;
         return high;
      }

      
      public static int Main(string[] args)
      {
         /*
      
          //HARDCODED TESTCASE 
         
         List<Zaag> A = new List<Zaag>();
         
         A.Add(new Zaag(4, true));
         A.Add(new Zaag(1, true));
         A.Add(new Zaag(2, true));
         A.Add(new Zaag(4, false));
         A.Add(new Zaag(6, false));
         A.Add(new Zaag(3, false));
         A.Add(new Zaag(1, false));

         //quickSort(A,0,A.Count-1,3);
         selectionSort(A,0,A.Count-1);

         foreach (Zaag i in A)
         {
            Console.Write(i.prijs + " ");
         }
         
         
         
         Console.WriteLine(" ");
         */

         
         
         var input = Console.ReadLine().Split(' ');
         int hoogte =  int.Parse(input[0]);
         int breedte = int.Parse(input[1]);
         int K =       int.Parse(input[2]);

         int counter = 0;
         List<Zaag> A = new List<Zaag>();
         while (counter < hoogte-1)
         {
            A.Add(new Zaag(int.Parse(Console.ReadLine().Split(' ')[0]),true));
            counter++;
         }

         counter = 0;
         while (counter < breedte-1)
         {
            A.Add(new Zaag(int.Parse(Console.ReadLine().Split(' ')[0]),false));
            counter++;
         }

         
            //implement selection and quicksort 
         //A.Sort((a, b) => b.CompareTo(a));
         quickSort(A,0,A.Count-1,K);
         //A.Sort();
         
         
         
         
         
         
         
         
         /**
         foreach (Zaag i in A)
         {
            Console.Write(i.prijs+" ");
         }
         Console.WriteLine("\n");
         */
         
         
         
         
         long cuts_horizontaal = 0;
         long cuts_verticaal = 0;
         long zaagsneden = 0;
         long som = 0;
         foreach (Zaag beweging in A)
         {
            
            if (beweging.horizontaal)
            {
               cuts_horizontaal++;
               zaagsneden += (cuts_verticaal+1);
               som += beweging.prijs * (cuts_verticaal + 1);
            }
            else
            {
               cuts_verticaal++;
               zaagsneden += (cuts_horizontaal+1);
               som += beweging.prijs * (cuts_horizontaal + 1);
            }
         }

         Console.WriteLine(som + "  " + zaagsneden);
         return 0;
      }



   }
    
    
}