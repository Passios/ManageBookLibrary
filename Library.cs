using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ManageBooksLib //dll
{
    public class Library 
    {

        public struct Book //δομή που θα είναι χρήσιμη για το καλάθι του χρήστη
        {
            public String ISBN;
            public int Quantity;
            public Double Price;
            public int Discount;
        }

        public struct ShippingInfo //δομη για τα στοιχειά αποστολής του πελάτη
        {
            public String Address;
            public String ZipCode;
            public String Region;
            public String Country;
        }

        public bool checkIsbnValid(string ISBN) //έλεγχος εαν είναι έγκυρο το ISBN // ttps://isbn-information.com/check-digit-for-the-13-digit-isbn.html
        {
            String isbn = ISBN; //το εκχωρούμε σε παράμετρο τύπου string
            List<int> temp = new List<int>(); //βοηθητική λίστα για να κάνυμε τους απαραίτητους υπολογισμους του isbn 1 προς 1 στοιχείο

            for (int i = 0; i < isbn.Length - 1; i++) //για κάθε στοιχείο του ISBN
            {
                int n = int.Parse(isbn[i].ToString()); //type casting κάθε ένα στοιχείο του string σε ακέραιο

                if (i % 2 != 0) //εάν το στοιχείο  mod 2 δεν είναι 0 (περιττό)
                {
                    temp.Add(n * 3); //το πολλαπλασιάζουμε με το 3 και το εκχωρούμε στην λίστα
                }
                else
                {
                    temp.Add(n); // αλλιως το εκχωρούμε στην λίστα
                }
            }
            
            int sum = 0; 
            foreach (int i in temp)
            {
                sum += i; //αθροιζουμε τα στοιχεια της βοηθητικής λίστας
            }

            int modulo = sum % 10; // παίρνουμε το υπόλοιπο απο την διαίρεση με το 10

            if (modulo == 0) //εάν είναι 0 δεν είναι εγκυρο το ISBN
            {
                return false;
            }
            else
            {
                int lastdigit = int.Parse(isbn[isbn.Length - 1].ToString()); // type casting παίρνουμε το τελαυταί ψηφίο απο το string (ISBN) και το κάνουμε ακέραιο
                if(10 - modulo == lastdigit) //εαν το τελευταιο ψηφίο είναι ίσο με 10-υπολοιπο τότε είναι έγκυρο 
                    return true;
            }

            return false;
        }

        public void CheckCardNum(string CardID, ref bool ValidCard, ref string CardType) // η μέθοδος αυτή δέχεται το id της κάρτας και ελέγχει την εγκυρότητα της και απο το id αναγνωρίζει τον τύπο της κάρτας
        {
            string id = CardID; //εκχωρούμε το id τύπου string της κάρτας
            int idLength = id.Length;  //παίρνουμε το μήκος της συμβολοσειράς

            // Mastercard, Visa, American Express, Maestro
            int[] mastercardLen = { 16 }; //η Mastercard έχει μήκος 16
            int[] visaLen = { 13, 16, 19 }; //κάνουμε το ίδιο για κάθε τυπου άλλη κάρτα
            int[] americanExpressLen = { 15 };
            int[] maestroLen = { 16, 17, 18, 19 };

            int[] mastercard = { 51, 52, 53, 54, 55 }; //η Mastercard ξεκιναει με ένα απο αυτους του ακέραιους
            int[] visa = { 4 }; // κάνουμε το ίδιο για κάθε τυπου άλλη κάρτα
            int[] americanExpress = { 34, 37 };
            int[] maestro = { 5018, 5020, 5038, 5893, 6304, 6759, 6761, 6762, 6763 };

            foreach(int i in mastercardLen) //για κάθε ένα απο τα στοιχεία 16 
            {
                if(idLength == i) //εαν τα στοιχεια για την Mastercard ειναι 16 
                {
                    ValidCard = true;      // θέτουμε την κάρτα εγκυρη              
                    foreach(int number in mastercard) //για κάθε ένα απ τα στοιχεια της mastercard
                    {
                        if(id.StartsWith(number.ToString())) //type casting απο int σε string , εαν ξεκιναει με κατι απ αυτα που αρχικοποιήσαμε παραπάνω
                        {
                            CardType = "MasterCard"; // είναι Mastercard
                        }
                    }
                    for(int j = 222100; j <= 272099; j++) //εαν ξεκιναει μεσα στο ευροσ των αριθμών αυτών 
                    {
                        string n = j.ToString(); //το μετατρέπουμε σε string γιατι η StartsWith δέχεται string ως όρισμα
                        if (id.StartsWith(n)) // εαν το id τησ καρτας ξεκινάει με το ανάλογο νουμερο που έχει η μεταβλητή n
                        {
                            CardType = "MasterCard"; // ειναι Mastercard
                        }
                    }
                }
            } 
            foreach(int i in visaLen) // το ίδιο και για τις άλλες κάρτες
            {
                if(idLength == i) //εάν το μέγεθος του string της κάρτας είναι ίσο με το visaLen
                {
                    ValidCard = true;                    
                    foreach(int number in visa)
                    {
                        if(id.StartsWith(number.ToString()))
                        {
                            CardType = "Visa";
                        }
                    }

                }
            }
            foreach(int i in americanExpressLen)
            {
                if(idLength == i)
                {
                    ValidCard = true;                    
                    foreach(int number in americanExpress)
                    {
                        if(id.StartsWith(number.ToString())) //εάν ξεκινάει με το αρχικοποιημένο
                        {
                            CardType = "American Express";
                        }
                    }
                }
            }
            foreach(int i in maestroLen)
            {
                if(idLength == i)
                {
                    ValidCard = true;                    
                    foreach(int number in maestro)
                    {

                        if(id.StartsWith(number.ToString()))
                        {
                            CardType = "Maestro";
                        }
                    }
                }
            }
        }

        public void CheckZipCode(ShippingInfo SI, ref bool ValidZC, ref bool ValidRg) //με την μέθοδο αυτή θα ελέγξουμε τα στοιχεια του πελάτη
        {
            string zip = String.Concat(SI.ZipCode.Where(c => !Char.IsWhiteSpace(c))); // παίρνουμε τον ταχυδρομικό κώδικα χωρίς κενά
            //ουσιαστικά το Where(c => !Char.IsWhiteSpace(c)) είναι for και το !Char.IsWhiteSpace(c) είναι μεθοδς της κλάσης Char για να παραλείπτει τα κενά
            int zipLen = zip.Length; //παίρνουμε το μήκος του ΤΚ
            string region = SI.Region; //κρατάμε και την περιοχή
            //string country = SI.Country;
            if (zipLen == 5) // εάν το μήκος του ΤΚ είναι 5
            {
                if (zip[0] >= '1' && zip[0] <= '8') //εαν το 1ο ψηφίο είναι απ 1 εως 8
                {
                    if (zip[1] >= '1' && zip[1] <= '8') //εαν το 2ο ψηφίο είναι απ 1 εως 8
                    {
                        if (zip[2] >= '1' && zip[2] <= '8') //εαν το 3ο ψηφίο είναι απ 1 εως 8
                        {
                            if (zip[3] >= '1' && zip[3] <= '8') //εαν το 4ο ψηφίο είναι απ 1 εως 8
                            {
                                if (zip[4] >= '1' && zip[4] <= '8') //εαν το 5ο ψηφίο είναι απ 1 εως 8
                                {
                                    ValidZC = true; // τότε  ο ΤΚ είναι έγκυρος αλλιως εαν δεν ισχύουν δεν είναι έγκυρος
                                }
                                else
                                {
                                    ValidZC = false;
                                }
                            } else
                            {
                                ValidZC = false;
                            }
                        } else
                        {
                            ValidZC = false;
                        }
                    } else
                    {
                        ValidZC = false;
                    }
                } else
                {
                    ValidZC = false;
                }
            // if (country.Equals("Ελλάδα") && ValidZC == true){
                if (region.Equals("Αττική") && ValidZC == true) // εάν η περιοχή είναι Αττική 
                {
                    ValidRg = true; //τότε η περιοχή είναι εγκυρή
                } else if (region.Equals("Ηπειρωτική") && ValidZC == true) // το ίδιο και για της άλλες περιοχές
                {
                    ValidRg = true;
                } else if (region.Equals("Νησί") && ValidZC == true)
                {
                    ValidRg = true;
                }
            //}
            /* else if (region.Equals("Εξωτερικό") && ValidZC == true) //το βάλαμε σε σχόλια για της ανάγκες της εργασίας (υποτίθεται οτι είναι παράλειψη του προγραμματιστη)
                {
                    ValidRg = true;
                } */
            else
            {
                ValidRg = false;
            }

            } else
            {
                ValidZC = false;
                ValidRg = false;
            }
        }

        //dt.Rows[i].Field<int>(j);
        // isbn -> 0
        // Stock -> 1 
        public bool IsAvailable(DataTable BooksStock, String ISBN, int quantity, ref int Stock) // η μέθοδος αυτή θα εξετάσει εάν τα βιβλία που θέλει ο χρήστης είναι διαθέσιμα
        { //το DataTable αποτελεί έναν πίνακα τον οποίον θα χρησιμοποιήσουμε σαν υποτιθέμενη βάση δεδομένων του βιβλιοπωλείου
            foreach (DataRow bookStock in BooksStock.Rows) { //για κάμία τιμή στον πίνακα
                if (bookStock.Field<string>(0) == ISBN && bookStock.Field<int>(1) - quantity >= 0) //εάν υπάρχει το βιλιοό που ζητάει (έλεγχος μέσω ISBN) και υπάρχει και απόθεμα μεγαλύτερο ή ίσο απο την ποσότητα που ζητάει ο χρήστης
                {
                    Stock = bookStock.Field<int>(1) - quantity; // ενημερώνεται το stock
                    return true; //η μέδοσος επιστρέφει true
                } else if(bookStock.Field<string>(0) == ISBN && bookStock.Field<int>(1) - quantity < 0) //αλλιώς false
                {
                    Stock = bookStock.Field<int>(1);
                    Console.WriteLine("Stock = {0}", Stock);
                    return false;
                }
            }
            return false;
        }

        public double BooksCost(Book[] Cart) //με αυτή την μέθοδο θα υπολογίσουμε το κόστος απο το καλάθι του πελάτη
        {
            double price = 0;
            for (int i = 0; i < Cart.Length; i++) // για κάθεσ τοιχείο του καλαθιού
            {
                int discount = Cart[i].Discount; //παιρνουμε την έκτπωση για να υπολογίσουμε το κόστος
                price = price + (Cart[i].Price - (Cart[i].Price * discount / 100)); //συναθροίζουμε τα κόστοι των προϊόντων συμπεριλαμβάνοντας την έκτωση 
            }
            return price; //επιστρέφουμε την συνολική τιμή
        }

        public double checkShippingCost(ShippingInfo si) //η μέθοδος αυτή θα επιστρέψει το κόστος μεταφοράς 
        {
            double ShippingCost = 0;
            if (si.Region == "Αττική") // εαν η περιοχή είναι η Αττική 
            {
                ShippingCost = 0; // δωρεάν μεταφορικά
                return ShippingCost;

            }
            else if (si.Region == "Νησί") // κάνουμε το ίδιο για κάθε περιοχή σύμφωνα με την εκφώνηση
            {
                ShippingCost = 5;
                return ShippingCost;
            }
            else if (si.Region == "Ηπειρωτική")
            {
                ShippingCost = 3.5;
                return ShippingCost;
            }
            else
            {
                return 8.5;
            }

        }

        // BookStock = Stock of books 
        // Cart = Books to buy
        public bool ProceedToBuy(DataTable BooksStock, Book[] Cart) //με την μέθοδο αυτή ελέγχουμε εάν τα βιβλία που θέλει ο χρήστης είναι διαθέσιμα
        { //ουσιαστικά θα εξετάσουμε μέσι της μεθόδου IsAvailable κάθε βιβλίο του καλαθιού εάν υπάρχει στην ανλάλογη ποσότητα που ζητά ο πελάτης στο καλάθι
            int count = 0;
            foreach(var book in Cart) // για κάθε ένα απο τα στοιχεία του καλαθιού
            {
                int stock = 0;
                if(IsAvailable(BooksStock, book.ISBN, book.Quantity, ref stock))
                {
                    count++; //εάν υπάρχει αυξάνουμε έναν μετρητή
                }
            }

            if (count == Cart.Length) //εάν ο μετρητής είναι ίσος με τα στοιχεία του καλαθιού
            {
                return true; //επιστρέφει true η μέθοδος
            }

            return false;
        }

        public void UpdateCartBasedOnAvailability(DataTable BooksStock, ref Book[] Cart) // με την μέθοδο αυτή θα εμημερώσυμε το καλάθι (ποσότητα βιβλίων) του πελάτη σε περίπτωση που ζητήσει βιβλία περισσότερα απο το απόθεμα
        {
            for(int book = 0; book < Cart.Length; book++) // εξετάζουμε 1 προσ 1 τα στοιχεία του καλαθιού
            {
                int stock = 0;
                if(!IsAvailable(BooksStock, Cart[book].ISBN, Cart[book].Quantity, ref stock)) //εάν δεν είναι διαθέσιμο στην ποσοτητα που ζητάει ο πελάτης
                {
                    Cart[book].Quantity = stock; //του ενημερώνουμε την ποσότητα με βάση το απόθεμα
                } 
            }
        }
    }
}
