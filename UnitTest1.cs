using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManageBooksLib;
using System.Data;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1() //έλεγχος μονάδας για την μέθοδο εγκυρότητας του ISBN
        {
            ManageBooksLib.Library store = new ManageBooksLib.Library();

            object[,] testData = //τύπου object γιατί περιέχει διάφορους τύπους δεδομένων
            {
                { true, "9781861972712" }, // το 9781861972712 είναι έγκυρο ISBN 
                { false, "9781681972712" }, 
                { true,  "9786188227132" },
               // { false, "sdfjasjnmfklsadklfjsdak" },
                //{ false, "a" }, 
                { false, "123213412234" },
                { false, "1000" }
            };

            int i = 0;
            try
            {
                for (i = 0; i < testData.GetLength(0); i++) // για κάθε ένα απο τα στοιχεία του πίνακα που δημιουργήσαμε
                {
                    Assert.AreEqual(store.checkIsbnValid((string)testData[i, 1]), testData[i, 0]); //ελέγχουμε μέσω της αντιστοιχης μεθόδου εάν επιστρέφει το αναμενόμενο αποτέλεσμα
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed test case: {0} | Reason: {1}", (string)testData[i, 1], e.Message); //εάν κάτι στροβώσει βλέπυμε εδώ το σφάλμα
            }
        }

        [TestMethod]
        public void TestMethod2() //έλεγχος μονάδας για την μέθοδο εγκυρότητας της κάρτας
        {
            ManageBooksLib.Library store = new ManageBooksLib.Library();

            object[,] testData = //τύπου object γιατί περιέχει διάφορους τύπους δεδομένων
            {
                // Valid Card, CardType, TestData
                { true, "Visa", "4539426199518811"},
                { true, "MasterCard", "2720994576392582" },
                { true, "American Express", "370976598481136" },
                { true, "Maestro", "6763841738638841" }//,
                //{ true, "Visa", "2792582"}
            };

            int i = 0;
            try
            {
                string cardType = "";
                bool isValid = false;
                for (i = 0; i < testData.GetLength(0); i++)
                {
                    isValid = false; // αρχικοποίηση επειδη είναι με αναφορά
                    cardType = "";
                    store.CheckCardNum((string)testData[i, 2], ref isValid, ref cardType); // ελέγχουμε την κάρτα μέσω της αντίστοιχης μεθόδου
                    //το πρώτο όρισμα είναι το id της κάρτας
                    Assert.AreEqual(isValid, testData[i, 0]);
                    Assert.AreEqual(cardType, testData[i, 1]);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed test case: index [{0}] data [{1}] | Reason: {2}", i, (string)testData[i, 1], e.Message); //εδω θα δουμε το σφάλμα και τον λόγο του σφάλματος σε περίπτωση λάθους

            }
        }

        [TestMethod]
        public void TestMethod3() //έλεγχος μονάδας για την μέθοδο που ελέγχει τα στοιχεία αποστολής 
        {
            ManageBooksLib.Library store = new ManageBooksLib.Library();

            Library.ShippingInfo nisi = new Library.ShippingInfo(); //φτίαχουμε ένα αντικοίμενο της δομής ShippingInfo για νησί
            nisi.Country = "Ελλάδα";
            nisi.Address = "Μήλου";
            nisi.ZipCode = "14732";
            nisi.Region = "Νησί";

            Library.ShippingInfo attiki = new Library.ShippingInfo(); //φτίαχουμε αντικοίμενα της δομής ShippingInfo και για τις άλλες περιπτώσεις
            attiki.Country = "Ελλάδα";
            attiki.Address = "Ματρόζου";
            attiki.ZipCode = "18452";
            attiki.Region = "Αττική";

            Library.ShippingInfo hpeirotiki = new Library.ShippingInfo();
            hpeirotiki.Country = "Ελλάδα";
            hpeirotiki.Address = "Καρπενήσι";
            hpeirotiki.ZipCode = "13128";
            hpeirotiki.Region = "Ηπειρωτική";

            Library.ShippingInfo eksoteriko = new Library.ShippingInfo();
            eksoteriko.Country = "Ισπανία";
            eksoteriko.Address = "Barca";
            eksoteriko.ZipCode = "121233";
            eksoteriko.Region = "Εξωτερικό";

            object[,] testData = //τύπου object γιατί περιέχει διάφορους τύπους δεδομένων
            {
                // Shipping Info Struct, ZoneCode, RegionCode
                { nisi,  true , true},
                { attiki, true, true},
                { hpeirotiki , true, true },
                //{ hpeirotiki , true, true }
                //{ eksoteriko, true, true }
              
            };

            int i = 0;
            try
            {
                bool validZoneCode = false; //αρχικοποιήσεις επειδή είναι με αναφορά, αλλιως πετάει σφάλμα
                bool validRegionCode = false;
                for (i = 0; i < testData.GetLength(0); i++)
                {
                    //validZoneCode = false;
                    //validRegionCode = false; 
                    store.CheckZipCode((Library.ShippingInfo)testData[i, 0], ref validZoneCode, ref validRegionCode); //με την αντιστοιχη μέθοδ ελέγχουμε τα στοιχεία αποστολής του πελάτη
                    // το πρώτο όρισμα είναι τα στοιχεία αποστολής του πελάτη
                    Assert.AreEqual(validZoneCode, testData[i, 1]); //εάν αυτά που επιστρέψει η μέθοδος είναι ιδια με αυτά που αρχικοποήσαμε
                    Assert.AreEqual(validRegionCode, testData[i, 2]);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed test case: index [{0}] data [{1}] | Reason: {2}", i, e.Message, testData[i, 1]); //εδω θα δουμε το σφάλμα και τον λόγο του σφάλματος σε περίπτωση λάθους

            }
        }


        [TestMethod]
        public void TestMethod4() //έλεγχος μονάδας για την μέθοδο για την διαθεσιμότητα του βιβλίου
        {
            ManageBooksLib.Library store = new ManageBooksLib.Library();
            // We have to Include DataTable into the Assembly Reference
            DataTable dt = new DataTable(); //η δομή DataTable αποτελεί έναν πίνακα στον οποίο θα εκχωρείσουμε δυναμικά ISBN βιβλίων και την διαθέσιμη ποσότητα

            dt.Columns.Add("ISBN", typeof(string)); //η πρώτη στήλη είναι τύπου string και θα περιέχει τα ISBN 
            dt.Columns.Add("Stock", typeof(int)); //η δεύτερη στήλη θα περιέχει το απόθεμα του αντίστοιχου βιβλίου
            dt.Rows.Add("9781681972712", 2); // βάζουμε τιμές στην δομή
            dt.Rows.Add("9781681972718", 15);
            dt.Rows.Add("9781681972711", 29);

            object[,] testData = //τύπου object γιατί περιέχει διάφορους τύουσ δεδομένων
            {
                // Test result, Book Stock, Isbn, quanitty, number Stock
                { false, dt, "9781681972712", 3 },
                { true, dt, "9781681972718", 15 },
                { true, dt, "9781681972711", 29},
            };

            int i = 0;
            try
            {
                int stock = 0;
                bool result = false;
                for (i = 0; i < testData.GetLength(0); i++)
                {
                    stock = 0;
                    result = store.IsAvailable((DataTable)testData[i, 1], (string)testData[i, 2], (int)testData[i, 3], ref stock); // μέσω της μεθόδου IsAvailable ελέγχουε εάν η ποσότητα που ζητάει ο πελάτης υπάρχει στο απόθεμα
                    Assert.AreEqual(testData[i, 0], result); // αυτό περιμένουμε να επιστρέψει για κάθε μία κληση
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed test case: index [{0}] data [{1}] | Reason: {2}", i, (string)testData[i, 1], e.Message); //εδω θα δουμε το σφάλμα και τον λόγο του σφάλματος σε περίπτωση λάθους

            }
        }

        [TestMethod]
        public void TestMethod5() //έλεγχος μονάδας για τον υπολογισμό του κόστους παραγγελίας
        {
            ManageBooksLib.Library store = new ManageBooksLib.Library();
            // We have to Include DataTable into the Assembly Reference

            ManageBooksLib.Library.Book[] Cart = new ManageBooksLib.Library.Book[3]; //δημιουργούμε το καλάθι του πελάτη που θα περάσουμε δυναμικά τιμές

            Cart[0].Price = 35; //με βάση αυτά θα υλογίσουμε το κόστος
            Cart[0].Quantity = 2;
            Cart[0].Discount = 10;
            Cart[1].Price = 14;
            Cart[1].Quantity = 10;
            Cart[1].Discount = 50;
            Cart[2].Price = 100;
            Cart[2].Quantity = 28;
            Cart[2].Discount = 20;

            object[,] testData = // τύπου object γιατι περιέχει διάφορους τύπους δεδομένων
            {
                // Test Result, Books or book, resulted Price
                // Checking one book at a time
                { 31.5 + 7 + 80 , Cart } //τιμή που πρέπει να επιστρέψει η μέθοδος, τιμή που επιστρέφει
                //{ true, Cart[1] },
                //{ true, Cart[2] },
            };

            int i = 0;
            try
            {
                double result = 0;
                for (i = 0; i < testData.GetLength(0); i++)
                {
                    result = 0;
                    result = store.BooksCost(Cart); //περνάμε την τιμή σε μεταβλητή τύπου double
                    Assert.AreEqual(testData[i, 0], result); //ελέγχουμε εάν είναι ίδιες
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed test case: index [{0}] data [{1}] | Reason: {2}", i, testData[i, 0], e.Message); //εδω θα δουμε το σφάλμα και τον λόγο του σφάλματος σε περίπτωση λάθους

            }
        }

        [TestMethod]
        public void TestMethod6() //έλεγχος μονάδας για την μέθοδο υπολογισμού εξόδων αποστολής
        {
            ManageBooksLib.Library cal = new ManageBooksLib.Library();

            Library.ShippingInfo nisi = new Library.ShippingInfo(); // δημιυργούμε αντικείμενα της δομής ShippingInfo για να υπολογίσουμε το κόστος
            nisi.Country = "Ελλάδα";
            nisi.Address = "Πάρνηθως";
            nisi.ZipCode = "14532";
            nisi.Region = "Νησί";

            Library.ShippingInfo attiki = new Library.ShippingInfo();
            attiki.Country = "Ελλάδα";
            attiki.Address = "Πάρνηθως";
            attiki.ZipCode = "18452";
            attiki.Region = "Αττική";

            Library.ShippingInfo[] values1 =
            {
                nisi, attiki
            };
            Double[] values2 =
            {
                5, 0
            };

            int i = 0;
            try
            {
                for (i = 0; i < values1.GetLength(0); i++)
                {
                    Double result = cal.checkShippingCost(values1[i]); //κάνουμε κλήση για να δούμε εάν κάθε περιοχή επιστρέφει τα σωστά έξοδα απστολής, επιστρέφει το κόστος αποστολής και το περνάνει σε μεταβλητή
                    Assert.AreEqual(values2[i], result);  //ελέγχουμε εάν επιστρέφει το επιθυμητό
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed test case: {0} | Reason: {1}", values1[i].Region, e.Message); //εδω θα δουμε το σφάλμα και τον λόγο του σφάλματος σε περίπτωση λάθους
            } 
        }


        [TestMethod]
        public void TestMethod7() //έλεγχος μονάδας για τον υπολογισμό του συνόλου μαζι με την εκτωση προϊόντων του καλαθιού
        {
            ManageBooksLib.Library store = new ManageBooksLib.Library();
            // We have to Include DataTable into the Assembly Reference
            DataTable dt = new DataTable(); //η δομή DataTable αποτελεί έναν πίνακα στον οποίο θα εκχωρείσουμε δυναμικά ISBN βιβλίων και την διαθέσιμη ποσότητα


            dt.Columns.Add("ISBN", typeof(string));
            dt.Columns.Add("Stock", typeof(int));
            dt.Rows.Add("9781681972712", 2);
            dt.Rows.Add("9781681972718", 15);
            dt.Rows.Add("9781681972711", 29);

            ManageBooksLib.Library.Book[] Cart = new ManageBooksLib.Library.Book[3];
            Cart[0].ISBN = "9781681972712";
            Cart[0].Price = 35;
            Cart[0].Quantity = 2;
            Cart[0].Discount = 10;
            Cart[1].ISBN = "9781681972718";
            Cart[1].Price = 14;
            Cart[1].Quantity = 10;
            Cart[1].Discount = 50;
            Cart[2].ISBN = "9781681972711";
            Cart[2].Price = 100;
            Cart[2].Quantity = 28;
            Cart[2].Discount = 20;

            object[,] testData =
           {
                // Test result, Book Stock, Isbn, quanitty, number Stock
                { true , dt, Cart }

            };

            int i = 0;
            try
            {

                bool result = false;
                for (i = 0; i < testData.GetLength(0); i++)
                {

                    result = store.ProceedToBuy((DataTable)testData[i, 1], Cart);
                    Assert.AreEqual(testData[i, 0], result);
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed test case: index [{0}] data [{1}] | Reason: {2}", i, (DataTable)testData[i, 1], e.Message); //εδω θα δουμε το σφάλμα και τον λόγο του σφάλματος σε περίπτωση λάθους

            }
        }


        [TestMethod]
        public void TestMethod8() //έλεγχος μονάδας για τον προσδιορισμό των στοιχείων στο κάλάθι ανάλογα με την διαθεσιμότηττα τους
        {
            ManageBooksLib.Library store = new ManageBooksLib.Library();
            // We have to Include DataTable into the Assembly Reference
            DataTable dt = new DataTable();

            dt.Columns.Add("ISBN", typeof(string)); //η πρώτη στήλη είναι τύπου string και θα περιέχει τα ISBN 
            dt.Columns.Add("Stock", typeof(int)); //η δεύτερη στήλη θα περιέχει το απόθεμα του αντίστοιχου βιβλίου
            dt.Rows.Add("9781681972712", 2); // εκχωρούμε δυναμικά τα αποθέματα
            dt.Rows.Add("9781681972718", 15);
            dt.Rows.Add("9781681972711", 29);

            ManageBooksLib.Library.Book[] Cart = new ManageBooksLib.Library.Book[3]; // θα εκχωρήσουμε αντικείμενα  στο καλάθι του πελάτη 
            Cart[0].ISBN = "9781681972712";
            Cart[0].Price = 35;
            Cart[0].Quantity = 3;
            Cart[0].Discount = 10;
            Cart[1].ISBN = "9781681972718";
            Cart[1].Price = 14;
            Cart[1].Quantity = 10;
            Cart[1].Discount = 50;
            Cart[2].ISBN = "9781681972711";
            Cart[2].Price = 100;
            Cart[2].Quantity = 28;
            Cart[2].Discount = 20;

            // Specific Book, Expected Result
            int[,] correctResults = { //το κλάθι πρέπει να ενημερωθεί σε περίτωση που ο χρήστης ζητάει αράνω απο τα αποθέματα
               { 0, 2 },
               { 1, 10 },
               { 2, 28 }
            }; 
            store.UpdateCartBasedOnAvailability(dt, ref Cart);  //μέθοδος που ενημερώνει το καλάθι

            int i = 0;
            try
            {

                for (i = 0; i < correctResults.GetLength(0); i++) //ενα προς ενα τα στοιχεία του καλαθιού
                {
                    Assert.AreEqual(correctResults[i, 1], Cart[correctResults[i, 0]].Quantity);  //ελέγχουμε ενάς έχει ενημερωθεί σωστά // πχ Cart[0].Quantity πρέπει να έγινε 2
                }
            }
            catch (Exception e)
            {
                Assert.Fail("failed test case: index [{0}] -> Expected : {1} and Got : {2}", i, correctResults[i, 1], Cart[correctResults[i, 0]].Quantity); //εδω θα δουμε το σφάλμα και τον λόγο του σφάλματος σε περίπτωση λάθους

            }
        }

    }
}

