using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace Quiz
{
    public class QuizData
    {
        public double Score { get; set; }
        public double Vysledek { get; set; }
        public string Priklad { get; set; }
    }

    public partial class MainWindow : Window
    {
        // Název souboru pro ukládání dat
        private const string DataFilePath = "quizdata.json";

        public MainWindow()
        {
            InitializeComponent();

            // Zkontrolovat, zda existuje soubor s daty
            if (File.Exists(DataFilePath))
            {
                NacistData(); // Načíst data při spuštění aplikace
            }
            else
            {
                GenerovatPriklad();
            }

            GenerovatVysledky();

            // Přiřadit událostní obsluhy tlačítek
            BTN1.Click += StiskTlacitka;
            BTN2.Click += StiskTlacitka;
            BTN3.Click += StiskTlacitka;
        }

        double score = 1;
        public static double vysledek;
        double obtiznost;

        // Generování nového příkladu
        public void GenerovatPriklad()
        {
            string priklad;
            int a;
            int b;
            string[] znamenka;
            string znamenko;

            // Nastavení obtížnosti podle skóre
            znamenka = new string[] { "+", "-", "*", "/" };
            obtiznost = 0.4 * score;

            Random rnd = new Random();
            a = rnd.Next(-10, (int)(obtiznost * 10));
            b = rnd.Next(1, (int)(obtiznost * 10));
            znamenko = znamenka[rnd.Next(0, 4)];

            // Výpočet správné odpovědi podle vybraného znaménka
            switch (znamenko)
            {
                case "+":
                    vysledek = a + b;
                    break;
                case "-":
                    vysledek = a - b;
                    break;
                case "*":
                    vysledek = a * b;
                    break;
                case "/":
                    vysledek = a / b;
                    break;
            }

            // Sestavení textového zápisu příkladu
            priklad = a.ToString() + znamenko + b.ToString();

            // Aktualizace obsahu labelu s příkladem a skórem
            Priklad_Label.Content = priklad;
            ScoreLabel.Content = "Score: " + score;

            UlozitData(); // Uložení dat po vygenerování nového příkladu
        }

        // Generování možných odpovědí
        public void GenerovatVysledky()
        {
            double spatnyVysledekA;
            double spatnyVysledekB;
            double spravnyVysledek = vysledek;
            int a;
            int b;
            Random rnd = new Random();

            // Vygenerování dvou nesprávných odpovědí
            do
            {
                a = rnd.Next(-10, 20);
                b = rnd.Next(-15, 15);
            } while (a == b || a == (int)spravnyVysledek || b == (int)spravnyVysledek);

            // Sestavení tří možných odpovědí
            spatnyVysledekA = vysledek + a;
            spatnyVysledekB = vysledek + b;

            List<double> vysledky = new List<double> { spatnyVysledekA, spatnyVysledekB, spravnyVysledek };
            vysledky = vysledky.OrderBy(x => Random.Shared.Next()).ToList();

            // Nastavení obsahu tlačítek podle vygenerovaných odpovědí
            BTN1.Content = vysledky[0].ToString();
            BTN2.Content = vysledky[1].ToString();
            BTN3.Content = vysledky[2].ToString();
        }

        // Obsluha stisku tlačítek
        public void StiskTlacitka(object sender, RoutedEventArgs e)
        {
            Button stiskleTlacitko = (Button)sender;
            string obsahTlacitka = stiskleTlacitko.Content.ToString();

            // Kontrola, zda je vybraná odpověď správná
            if (obsahTlacitka == vysledek.ToString())
            {
                score += 1;
                GenerovatPriklad();
                GenerovatVysledky();
            }
            else
            {
                score = 1;
                GenerovatPriklad();
                GenerovatVysledky();
            }
        }

        // Uložení aktuálního skóre a příkladu do JSON souboru
        private void UlozitData()
        {
            try
            {
                var quizData = new QuizData { Score = score, Vysledek = vysledek, Priklad = Priklad_Label.Content.ToString() };
                string jsonData = JsonConvert.SerializeObject(quizData);

                File.WriteAllText(DataFilePath, jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při ukládání dat: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Načtení skóre a příkladu ze souboru při spuštění aplikace
        private void NacistData()
        {
            try
            {
                if (File.Exists(DataFilePath))
                {
                    string jsonData = File.ReadAllText(DataFilePath);
                    var quizData = JsonConvert.DeserializeObject<QuizData>(jsonData);

                    if (quizData != null)
                    {
                        score = quizData.Score;
                        vysledek = quizData.Vysledek;
                        Priklad_Label.Content = quizData.Priklad;
                        ScoreLabel.Content = "Score: " + score;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při načítání dat: {ex.Message}", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
