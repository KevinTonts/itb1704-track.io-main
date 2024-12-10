using BackEnd.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Test.itb1704.BackEnd.Model;

namespace Test.itb1704.BackEnd.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Exercise>? ExerciseList { get; set; }
        public DbSet<History>? HistoryList { get; set; }
        public DbSet<Nutrition>? NutritionList { get; set; }
        public DbSet<MealPlan>? MealPlans { get; set; }
        public DbSet<WorkoutPlan>? WorkoutPlans { get; set; }
        public DbSet<WorkoutPlanExercise>? WorkoutPlanExercises { get; set; }
        public DbSet<MealPlanNutrition>? MealPlanNutritions { get; set; }
        public DbSet<User>? UserList { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            // mb.Entity<History>().ToTable("History").HasKey(key => new { key.ExerciseId, key.NutritionId });
            mb.Entity<History>().ToTable("History").HasKey(x => x.Id);
            mb.Entity<Exercise>().ToTable("Exercises").HasKey(x => x.Id);
            mb.Entity<Nutrition>().ToTable("Nutritions").HasKey(x => x.Id);
            mb.Entity<MealPlan>().ToTable("MealPlans").HasKey(x => x.Id);
            mb.Entity<MealPlanNutrition>().ToTable("MealPlanNutritions").HasKey(key => new { key.MealPlanId, key.NutritionId });
            mb.Entity<WorkoutPlan>().ToTable("WorkoutPlans").HasKey(x => x.Id);
            mb.Entity<WorkoutPlanExercise>().ToTable("WorkoutPlanExercises").HasKey(key => new { key.ExerciseId, key.WorkoutPlanId });
            mb.Entity<ExerciseNutritionHistory>().ToTable("ExerciseNutritionHistories").HasKey(key => new { key.ExerciseId, key.HistoryId, key.NutritionId });
            mb.Entity<NutritionHistroy>().ToTable("NutritionHistroies").HasKey(key => new { key.NutritionId, key.HistoryId });
            mb.Entity<User>().ToTable("Users").HasKey(x => x.Id);

            mb.Entity<Exercise>().Property(p => p.Id).HasIdentityOptions(startValue: 13);
            mb.Entity<History>().Property(p => p.Id).HasIdentityOptions(startValue: 4);
            mb.Entity<Nutrition>().Property(p => p.Id).HasIdentityOptions(startValue: 4);


            mb.Entity<Organization>().HasData(
                new Organization
                {
                    Id = 1
                },
                new Organization
                {
                    Id = 2
                }
            );

            mb.Entity<Exercise>().HasData(
                // Outside
                new Exercise
                {
                    Id = 1,
                    Title = "Kätekõverdused jala tõstega",
                    Description = "Tavalised kätekõverdused korda mööda jalga tõstes",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 100,
                    WeekDay = DayOfTheWeek.Esmaspäev,
                    OrganizationId = 1
                },
                new Exercise
                {
                    Id = 2,
                    Title = "Slaalomhüpped",
                    Description = "Kükist hüpped küljelt küljele",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 200,
                    WeekDay = DayOfTheWeek.Teisipäev,
                    OrganizationId = 2
                },
                new Exercise
                {
                    Id = 3,
                    Title = "Alt läbi jooks",
                    Description = "Toenglamangus jooksmine",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 300,
                    WeekDay = DayOfTheWeek.Kolmapäev,
                    OrganizationId = 1
                },
                new Exercise
                {
                    Id = 4,
                    Title = "Lõuatõmbed",
                    Description = "Lõuatõmbed kangi peal keha üles tõmbamine",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 90,
                    WeekDay = DayOfTheWeek.Neljapäev,
                    OrganizationId = 2
                },
                new Exercise
                {
                    Id = 5,
                    Title = "Kükid hüppega",
                    Description = "Kükist hüpped otse üles",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 60,
                    WeekDay = DayOfTheWeek.Reede,
                    OrganizationId = 1
                },
                // Outside
                // Gym
                new Exercise
                {
                    Id = 6,
                    Title = "Rinnalt surumine",
                    Description = "Rinnapingi juures rinnalt surumine",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 40,
                    WeekDay = DayOfTheWeek.Esmaspäev,
                    OrganizationId = 3
                },
                new Exercise
                {
                    Id = 7,
                    Title = "Sõudmine",
                    Description = "Masina peal sõudmine",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 300,
                    WeekDay = DayOfTheWeek.Teisipäev,
                    OrganizationId = 1
                },
                new Exercise
                {
                    Id = 8,
                    Title = "Üle pea kangi tõstmine",
                    Description = "Asetad kangi rinnal ja tõstad otse pea kohale",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 30,
                    WeekDay = DayOfTheWeek.Kolmapäev,
                    OrganizationId = 1
                },
                new Exercise
                {
                    Id = 9,
                    Title = "Sangpommiga number 8",
                    Description = "Viid sangpommi jalgevahelt läbi kätev ahetustega",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 55,
                    WeekDay = DayOfTheWeek.Neljapäev,
                    OrganizationId = 1
                },
                new Exercise
                {
                    Id = 10,
                    Title = "Hantlitega piitsa",
                    Description = "Peaks olema arusaadav",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 40,
                    WeekDay = DayOfTheWeek.Reede,
                    OrganizationId = 3
                },
                new Exercise
                {
                    Id = 11,
                    Title = "Hantlitega piitsa",
                    Description = "Peaks olema arusaadav",
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    BurnedCalories = 40,
                    WeekDay = DayOfTheWeek.Reede,
                    OrganizationId = 2
                },

                new Exercise
                {
                    Id = 12,
                    Title = "Hantlitega piitsa",
                    Description = "Peaks olema arusaadav",
                    Date = DateTime.UtcNow.Date.AddDays(-2).ToString("MM-dd-yyyy"),
                    BurnedCalories = 40,
                    WeekDay = DayOfTheWeek.Reede,
                    OrganizationId = 2,
                    ExerciseFinished = true //see testimiseks
                }

            // Gym
            );

            mb.Entity<Nutrition>().HasData(
                new Nutrition
                {
                    Id = 1,
                    Description = "Muna",
                    AmountInGrams = 175,
                    Carbohydrates = 1.35,
                    Fats = 17.4,
                    Proteins = 22.02,
                    TotalCalories = 257,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Hommikusöök,
                    WeekDay = WeekDay.Esmaspäev
                },
                new Nutrition
                {
                    Id = 2,
                    Description = "Sai",
                    AmountInGrams = 100,
                    Carbohydrates = 43,
                    Fats = 3.5,
                    Proteins = 12,
                    TotalCalories = 252,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Hommikusöök,
                    WeekDay = WeekDay.Esmaspäev
                },
                new Nutrition
                {
                    Id = 3,
                    Description = "Piim",
                    AmountInGrams = 250,
                    Carbohydrates = 100,
                    Fats = 4.662,
                    Proteins = 8.05,
                    TotalCalories = 150,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Hommikusöök,
                    WeekDay = WeekDay.Esmaspäev
                },
                new Nutrition
                {
                    Id = 4,
                    Description = "Kartulisalat",
                    AmountInGrams = 250,
                    Carbohydrates = 1.35,
                    Fats = 17.4,
                    Proteins = 22.02,
                    TotalCalories = 257,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Lõunasöök,
                    WeekDay = WeekDay.Esmaspäev
                },
                new Nutrition
                {
                    Id = 5,
                    Description = "Õun",
                    AmountInGrams = 150,
                    Carbohydrates = 43,
                    Fats = 3.5,
                    Proteins = 12,
                    TotalCalories = 252,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Snack,
                    WeekDay = WeekDay.Esmaspäev
                },
                new Nutrition
                {
                    Id = 6,
                    Description = "Kartul",
                    AmountInGrams = 150,
                    Carbohydrates = 1.35,
                    Fats = 17.4,
                    Proteins = 22.02,
                    TotalCalories = 257,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Õhtusöök,
                    WeekDay = WeekDay.Esmaspäev
                },
                new Nutrition
                {
                    Id = 7,
                    Description = "Hakklihakaste",
                    AmountInGrams = 100,
                    Carbohydrates = 43,
                    Fats = 3.5,
                    Proteins = 12,
                    TotalCalories = 252,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Õhtusöök,
                    WeekDay = WeekDay.Esmaspäev
                },
                // siit algab teisip
                new Nutrition
                {
                    Id = 8,
                    Description = "Puder",
                    AmountInGrams = 175,
                    Carbohydrates = 1.35,
                    Fats = 17.4,
                    Proteins = 22.02,
                    TotalCalories = 257,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Hommikusöök,
                    WeekDay = WeekDay.Teisipäev
                },
                new Nutrition
                {
                    Id = 9,
                    Description = "Piim",
                    AmountInGrams = 100,
                    Carbohydrates = 43,
                    Fats = 3.5,
                    Proteins = 12,
                    TotalCalories = 252,
                    Date = DateTime.UtcNow.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Hommikusöök,
                    WeekDay = WeekDay.Teisipäev
                },
                new Nutrition
                {
                    Id = 10,
                    Description = "Frikadellisupp",
                    AmountInGrams = 175,
                    Carbohydrates = 1.35,
                    Fats = 17.4,
                    Proteins = 22.02,
                    TotalCalories = 257,
                    Date = DateTime.Now.Date.ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Lõunasöök,
                    WeekDay = WeekDay.Teisipäev
                },
                new Nutrition
                {
                    Id = 11,
                    Description = "Kana ja seene Risotto",
                    AmountInGrams = 100,
                    Carbohydrates = 43,
                    Fats = 3.5,
                    Proteins = 12,
                    TotalCalories = 252,
                    Date = DateTime.Now.Date.AddDays(-2).ToString("MM-dd-yyyy"),
                    MealTime = MealTime.Lõunasöök,
                    WeekDay = WeekDay.Teisipäev,
                    NutritionFinished = true //see testimiseks
                }
            );


            mb.Entity<History>().HasData(
                new History
                {
                    Id = 1,
                    // ExerciseId = 1,
                    // NutritionId = 1,
                    Date = DateTime.UtcNow

                },
                new History
                {
                    Id = 2,
                    // ExerciseId = 2,
                    // NutritionId = 2,
                    Date = DateTime.UtcNow.Date.AddDays(-2)
                },
                new History
                {
                    Id = 3,
                    // ExerciseId = 3,
                    // NutritionId = 3,
                    Date = DateTime.UtcNow.Date.AddDays(-3)
                }
            );

            mb.Entity<MealPlanNutrition>().HasData(
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 1 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 2 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 3 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 4 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 5 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 6 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 7 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 8 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 9 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 10 },
                new MealPlanNutrition { MealPlanId = 1, NutritionId = 11 },
                new MealPlanNutrition { MealPlanId = 2, NutritionId = 2 },
                new MealPlanNutrition { MealPlanId = 2, NutritionId = 5 },
                new MealPlanNutrition { MealPlanId = 2, NutritionId = 7 },
                new MealPlanNutrition { MealPlanId = 2, NutritionId = 10 },
                new MealPlanNutrition { MealPlanId = 2, NutritionId = 11 }
            );

            mb.Entity<MealPlan>().HasData(
                new MealPlan { Id = 1, Name = "Hardcore bulking", LenghtInDays = 7 },
                new MealPlan { Id = 2, Name = "Kaalu langetamine", LenghtInDays = 7 },
                new MealPlan { Id = 3, Name = "Kaalu hoidmine", LenghtInDays = 7 }
            );

            mb.Entity<WorkoutPlanExercise>().HasData(
                new WorkoutPlanExercise() { ExerciseId = 6, WorkoutPlanId = 3 },
                new WorkoutPlanExercise() { ExerciseId = 7, WorkoutPlanId = 3 },
                new WorkoutPlanExercise() { ExerciseId = 8, WorkoutPlanId = 3 },
                new WorkoutPlanExercise() { ExerciseId = 9, WorkoutPlanId = 3 },
                new WorkoutPlanExercise() { ExerciseId = 10, WorkoutPlanId = 3 },
                new WorkoutPlanExercise() { ExerciseId = 1, WorkoutPlanId = 2 },
                new WorkoutPlanExercise() { ExerciseId = 2, WorkoutPlanId = 2 },
                new WorkoutPlanExercise() { ExerciseId = 3, WorkoutPlanId = 2 },
                new WorkoutPlanExercise() { ExerciseId = 4, WorkoutPlanId = 2 },
                new WorkoutPlanExercise() { ExerciseId = 11, WorkoutPlanId = 2 },
                new WorkoutPlanExercise() { ExerciseId = 12, WorkoutPlanId = 2 },
                new WorkoutPlanExercise() { ExerciseId = 5, WorkoutPlanId = 2 }
            );

            mb.Entity<WorkoutPlan>().HasData(
                new WorkoutPlan { Id = 1, Name = "Midagi algajatele", Description = "Põnev", ExercisePlanIntensity = Intensity.Algajatele, AgeCategory = Category.Lastele },
                new WorkoutPlan { Id = 2, Name = "Looduses", Description = "Üli põnev", ExercisePlanIntensity = Intensity.Edasijõudnutele, AgeCategory = Category.Täiskasvanutele },
                new WorkoutPlan { Id = 3, Name = "Jõusaal", Description = "MegaÜli põnev", ExercisePlanIntensity = Intensity.Proffidele, AgeCategory = Category.Täiskasvanutele }
            );

            mb.Entity<ExerciseNutritionHistory>().HasData(
                new ExerciseNutritionHistory { ExerciseId = 1, HistoryId = 1, NutritionId = 1 },
                new ExerciseNutritionHistory { ExerciseId = 2, HistoryId = 1, NutritionId = 2 },
                new ExerciseNutritionHistory { ExerciseId = 3, HistoryId = 1, NutritionId = 3 },
                new ExerciseNutritionHistory { ExerciseId = 3, HistoryId = 2, NutritionId = 4 },
                new ExerciseNutritionHistory { ExerciseId = 4, HistoryId = 1, NutritionId = 4 },
                new ExerciseNutritionHistory { ExerciseId = 1, HistoryId = 2, NutritionId = 1 }
            );

            mb.Entity<NutritionHistroy>().HasData(
                new NutritionHistroy { NutritionId = 1, HistoryId = 1 },
                new NutritionHistroy { NutritionId = 2, HistoryId = 1 },
                new NutritionHistroy { NutritionId = 3, HistoryId = 1 },
                new NutritionHistroy { NutritionId = 1, HistoryId = 2 },
                new NutritionHistroy { NutritionId = 4, HistoryId = 2 },
                new NutritionHistroy { NutritionId = 5, HistoryId = 2 }
            );

            mb.Entity<User>().HasData(
                new User { Id = 1, Username = "test1", Password = "St9tpNN2zrinRGNUgKWCy4JjZRFEorSQ0Zg3a/8m7k4=", OrganizationId = 1, FirstName = "name1", LastName = "last1", Height = 180, Weight = 80, Age = 15, Gender = Genders.Male }, //pw: test1
                new User { Id = 2, Username = "test2", Password = "zWoe4T9h2Hj9G4dyUtWwcKwV6zMR1Q0yr3Uch+xSze8=", OrganizationId = 2, FirstName = "name2", LastName = "last2", Height = 160, Weight = 50, Age = 25, Gender = Genders.Female }, //pw: test2
                new User { Id = 3, Username = "test3", Password = "6RwNz8ehCp0yZ0KkUE7i+Shy+2l7C1Eh9dT/RULwZN8=", OrganizationId = 3, FirstName = "name3", LastName = "last3", Height = 170, Weight = 70, Age = 20, Gender = Genders.Male } //pw test3
            );
        }
    }
}