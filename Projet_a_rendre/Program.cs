using System;
using TPCSharpàRendre.DataSources.Collections;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using System.Xml;
using System.Runtime.ConstrainedExecution;
using System.Net.Http.Json;

namespace TPCSharpàRendre
{
    class Program
    {
        static void Main(string[] args)
        {
            var allCars = ListCarsData.ListeVoitures;
            Console.WriteLine("Que veux-tu faire ? ");
            Console.WriteLine("1 - Convertir la donnée");
            Console.WriteLine("2 - Une recherche depuis une collection ou un Json");
            Console.WriteLine("3 - Un trie");

            string result = Console.ReadLine();

            switch (result)
            {
                case "1":
                    ConvertToJsonXmlOrTxt(allCars);
                    break;
                case "2":
                    Search();
                    break;
                case "3":
                    SortCarList(allCars);
                    break;
                default:
                    Console.WriteLine("Choix incorrecte");
                    break;
            }
        }

        public static void ConvertToJsonXmlOrTxt(List<Car> allCars)
        {
            Console.WriteLine("\nEn quoi voulez-vous transformer vos données ? ");
            Console.WriteLine("1 - Xml");
            Console.WriteLine("2 - Json");
            Console.WriteLine("3 - txt");

            string result = Console.ReadLine();

            switch (result)
            {
                case "1":
                    ConvertXML(allCars);
                    break;
                case "2":
                    ConvertJson(allCars);
                    break;
                case "3":
                    ConvertTxt(allCars);
                    break;
                default:
                    Console.WriteLine("Choix incorrecte");
                    break;
            }
        }

        public static void Search()
        {
            List<Car> allCars;
            Console.WriteLine("\nVous voulez faire votre recherche depuis : ");
            Console.WriteLine("1 - Une collection");
            Console.WriteLine("2 - Un fichier Json");
            string resultChoice = Console.ReadLine();
            
            if (resultChoice == "1")
            {
                allCars = ListCarsData.ListeVoitures;
            }
            else if (resultChoice == "2")
            {
                var path = @"C:\Users\odoux\Documents\LinQ\Projet_a_rendre\Projet_a_rendre\DataSources\JSON\search.json";
                string json = File.ReadAllText(path);
                allCars = JsonConvert.DeserializeObject<List<Car>>(json);
            }
            else
            {
                Console.WriteLine("Choix invalide. Veuillez choisir une option valide.");
                return;
            }

            Console.WriteLine("\nQue voulez-vous recherche : ");
            Console.WriteLine("1 - L'id");
            Console.WriteLine("2 - La marque");
            Console.WriteLine("3 - Le modèle");
            Console.WriteLine("4 - L'année de sortie");
            string result = Console.ReadLine();

            switch (result)
            {
                case "1":
                    Console.WriteLine("Entre l'id que vous rechercher : ");
                    string searchById = Console.ReadLine();
                    while (searchById.Length == 0)
                    {
                        Console.WriteLine("Rien n'est ecrit taper l'id pour la rechercher : ");
                        searchById = Console.ReadLine();
                    }
                    List<Car> resultSearchById = SearchById(allCars, int.Parse(searchById));
                    Display(resultSearchById);
                    break;
                case "2":
                    Console.WriteLine("Entre la marque que vous rechercher : ");
                    string searchByMarque = Console.ReadLine();
                    while (searchByMarque.Length == 0)
                    {
                        Console.WriteLine("Rien n'est ecrit taper la marque pour la rechercher : ");
                        searchByMarque = Console.ReadLine().ToLower();
                    }
                    List<Car> resultsearchByMarque = SearchByMarque(allCars, searchByMarque);
                    Display(resultsearchByMarque);
                    break;
                case "3":
                    Console.WriteLine("Entre le modele que vous rechercher : ");
                    string searchByModele = Console.ReadLine();
                    while (searchByModele.Length == 0)
                    {
                        Console.WriteLine("Rien n'est ecrit taper le modèle pour la rechercher : ");
                        searchByModele = Console.ReadLine();
                    }
                    List<Car> resultSearchByModele = SearchByModele(allCars, searchByModele);
                    Display(resultSearchByModele);
                    break;
                case "4":
                    Console.WriteLine("Entre l'année que vous rechercher : ");
                    string searchByYear = Console.ReadLine();
                    while (searchByYear.Length < 4)
                    {
                        Console.WriteLine("L'année de sortie est composée de quatre chiffre pour une voiture : ");
                        searchByYear = Console.ReadLine();
                    }
                    List<Car> resultSearchByYears = SearchByYears(allCars, int.Parse(searchByYear));
                    Display(resultSearchByYears);
                    break;
                default:
                    Console.WriteLine("Choix incorrecte");
                    break;
            }
        }

        public static void SortCarList(List<Car> allCars)
        {
            Console.WriteLine("\nQuel type de trie voulez-vous faire? ");
            Console.WriteLine("1 - Ordre croissant");
            Console.WriteLine("2 - Ordre décroissant");
            Console.WriteLine("3 - Faire un trie avec une condition");
            string result = Console.ReadLine();

            switch (result)
            {
                case "1":
                    Display(allCars);
                    break;
                case "2":
                    List<Car> resultOrderDescending = SortByDescending(allCars);
                    Display(resultOrderDescending);
                    break;
                case "3":
                    SortCarSpescific(allCars);
                    break;
                default:
                    Console.WriteLine("Choix incorrecte");
                    break;
            }
        }

        public static void SortCarSpescific(List<Car> allCars)
        {
            Console.WriteLine("\nLe trie se fera sur l'année? ");
            Console.WriteLine("1 - Supérieur à votre date de sortie");
            Console.WriteLine("2 - Inférieur à votre date de sortie");
            string result = Console.ReadLine();

            switch (result)
            {
                case "1":
                    Console.WriteLine("\nEntre l'année");
                    string resultSearchSuperior = Console.ReadLine();
                    while (resultSearchSuperior.Length < 4)
                    {
                        Console.WriteLine("L'année de sortie est composée de quatre chiffre pour une voiture : ");
                        resultSearchSuperior = Console.ReadLine();
                    }
                    List<Car> resultSuperior = SearchSuperior(allCars, int.Parse(resultSearchSuperior));
                    Display(resultSuperior);
                    break;
                case "2":
                    Console.WriteLine("\nEntre l'année");
                    string resultSearchInferior = Console.ReadLine();
                    while (resultSearchInferior.Length < 4)
                    {
                        Console.WriteLine("L'année de sortie est composée de quatre chiffre pour une voiture : ");
                        resultSearchInferior = Console.ReadLine();
                    }
                    List<Car> resultInferior = SearchInferior(allCars, int.Parse(resultSearchInferior));
                    Display(resultInferior);
                    break;
                default:
                    Console.WriteLine("Choix incorrecte");
                    break;
            }
        }

        public static void ConvertXML(List<Car> allCars)
        {
            XElement xml = new XElement("AllVoitures", from voit in allCars
                                                       select
                                                  new XElement("voiture",
                                                    new XAttribute("Id", voit.Id),
                                                    new XElement("Marque", voit.Marque),
                                                    new XElement("Modele", voit.Modele),
                                                    new XElement("Year", voit.Year)
                                                  ));
            XDocument xmlDoc = new XDocument(xml);
            string path = @"C:\Users\odoux\Documents\LinQ\Projet_a_rendre\Projet_a_rendre\DataSources\XML\voitures.xml";
            xmlDoc.Save(path);
            Console.WriteLine(xml.ToString());
        }
        public static void ConvertJson(List<Car> allCars)
        {
            var json = new JObject(new JProperty("AllVoiture",
                                        from voit in allCars
                                        select new JObject(
                                            new JProperty("Id", voit.Id),
                                            new JProperty("Marque", voit.Marque),
                                            new JProperty("Modele", voit.Modele),
                                            new JProperty("Year", voit.Year)
                                            )
                                        )
                                   );
            Console.WriteLine(json.ToString());
            var path = @"C:\Users\odoux\Documents\LinQ\Projet_a_rendre\Projet_a_rendre\DataSources\JSON\voitures.json";
            string jsonFile = JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(path, jsonFile);
        }
        public static void ConvertTxt(List<Car> allCars)
        {
            var voitureTxtList = from voit in allCars
                                 select $" La voiture numero {voit.Id} de la marque {voit.Marque} du modèle {voit.Modele} année {voit.Year} \n\n";
            string titleAllVoitures = "Voici toutes mes voitures : \n";
            foreach (var voit in voitureTxtList)
            {
                titleAllVoitures += voit;
            }

            Console.WriteLine(titleAllVoitures);
            string path = @"C:\Users\odoux\Documents\LinQ\Projet_a_rendre\Projet_a_rendre\DataSources\TXT\voitures.txt";
            File.WriteAllText(path, titleAllVoitures);
        }
        public static void Display(List<Car> allCars)
        {
            Console.WriteLine("Resulat de la recherches \n");
            foreach (var car in allCars)
            {
                Console.WriteLine($" La voiture numero {car.Id} de la marque {car.Marque} du modèle {car.Modele} année {car.Year} \n");
            }
        }
        public static List<Car> SearchByModele(List<Car> allCars, string searchByModele)
        {
            return allCars.Where(car => car.Modele.ToLower().Equals(searchByModele, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public static List<Car> SearchByMarque(List<Car> allCars, string searchByMarque)
        {
            return allCars.Where(car => car.Marque.ToLower().Equals(searchByMarque, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public static List<Car> SearchByYears(List<Car> allCars, int searchByYear)
        {
            return allCars.Where(car => car.Year == searchByYear).ToList();
        }
        public static List<Car> SearchById(List<Car> allCars, int searchById)
        {
            return allCars.Where(car => car.Id == searchById).ToList();
        }
        public static List<Car> SortByDescending(List<Car> allCars)
        {
            return allCars.OrderByDescending(car => car.Id).ToList();
        }
        public static List<Car> SearchSuperior(List<Car> allCars, int searchBySuperior)
        {
            return allCars.Where(car => car.Year > searchBySuperior).ToList();
        }
        public static List<Car> SearchInferior(List<Car> allCars, int searchByInferior)
        {
            return allCars.Where(car => car.Year < searchByInferior).ToList();
        }
    }
}