using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace CsenPomsaeScore
{
    /*
     * This class models an athlete.
     */
    public class Athlete
    {
        public enum Categories
        {
            [Description("NOVIZI")]
            NOVIZI,
            [Description("ESORDIENTI")]
            ESORDIENTI,
            [Description("CADETTI 1")]
            CADETTI_1,
            [Description("CADETTI 2")]
            CADETTI_2,
            [Description("JUNIOR")]
            JUNIOR,
            [Description("SENIOR 1")]
            SENIOR_1,
            [Description("SENIOR 2")]
            SENIOR_2,
            [Description("MASTER 1")]
            MASTER_1,
            [Description("MASTER 2")]
            MASTER_2
        };

        public enum Genders
        {
            [Description("M")]
            M,
            [Description("F")]
            F
        };

        public enum GradeGroups
        {
            [Description("10-9 KUP")]
            KUP_10_9,
            [Description("8-7 KUP")]
            KUP_8_7,
            [Description("6-5 KUP")]
            KUP_6_5,
            [Description("4-3 KUP")]
            KUP_4_3,
            [Description("2-1 KUP")]
            KUP_2_1,
            [Description("POOM")]
            POOM,
            [Description("DAN")]
            DAN
        };

        //Registry data
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Gym { get; set; }
        public String Nationality { get; set; }
        public Categories Category { get; set; }
        public Genders Gender { get; set; }
        public GradeGroups GradeGroup { get; set; }
        public String Grade { get; set; }

        //Competition data
        public int Round { get; set; }
        public TimeSpan? Time { get; set; }
        public bool PoomsaeCompleted { get; set; }
        public List<Double> JudgeVotes { get; set; }
        public double? MediumScore { get; set; }
        public double? TotalScore { get; set; }
        public bool HasFinished { get; set; }


        /// <summary>
        /// Constructs a new athlete.
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="name">Name</param>
        /// <param name="surname">Surname</param>
        /// <param name="gym">Former gym</param>
        public Athlete(int id, string name, string surname, string gym, string nationality, Categories category, Genders gender, GradeGroups gradeGroup, String grade)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Gym = gym;
            Nationality = nationality;
            Category = category;
            Gender = gender;
            GradeGroup = gradeGroup;
            Grade = grade;
            Round = 1;
            Time = null;
            PoomsaeCompleted = false;
            JudgeVotes = new List<double>();
            MediumScore = null;
            TotalScore = null;
            HasFinished = false;
        }

        /// <summary>
        /// This method returns the full name of the athlete.
        /// </summary>
        /// <returns>full name</returns>
        public string GetFullName()
        {
            return Name + " " + Surname;
        }

        /// <summary>
        /// This method returns the gender description of the athlete.
        /// </summary>
        /// <returns>gender</returns>
        public string GetGenderDescription()
        {
            return GetDescription<Genders>(Gender);
        }

        /// <summary>
        /// This method returns the category description of the athlete.
        /// </summary>
        /// <returns>category</returns>
        public string GetCategoryDescription()
        {
            return GetDescription<Categories>(Category);
        }

        /// <summary>
        /// Set all athlete data to zero.
        /// </summary>
        public void SetReceded()
        {
            Time = TimeSpan.Zero;
            for (int i = 0; i < Competition.MyCompetition.GetJudgeNumber(); i++)
                JudgeVotes.Add(0.0);
            TotalScore = 0.0;
            MediumScore = 0.0;
            PoomsaeCompleted = true;
            HasFinished = true;
        }

        /// <summary>
        /// Delete all athlete data.
        /// </summary>
        public void ClearData()
        {
            Time = null;
            PoomsaeCompleted = false;
            JudgeVotes.Clear();
            TotalScore = null;
            MediumScore = null;
            HasFinished = false;
        }

        /// <summary>
        /// Return the description associated to the specified enum element.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="enumerationValue">Enum element</param>
        /// <returns>Enum element description</returns>
        private static string GetDescription<T>(T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }

        /// <summary>
        /// Get the category item associated to the specified description.
        /// </summary>
        /// <param name="description">Category description</param>
        /// <returns>The enum item if the specified description exists, null otherwise</returns>
        public static Categories? GetCategoryFromDescription(string description)
        {
            foreach (Categories c in Enum.GetValues(typeof(Categories)))
            {
                if (GetDescription<Categories>(c) == description.ToUpper())
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the gender item associated to the specified description.
        /// </summary>
        /// <param name="description">Gender description</param>
        /// <returns>The enum item if the specified description exists, null otherwise</returns>
        public static Genders? GetGenderFromDescription(string description)
        {
            foreach (Genders c in Enum.GetValues(typeof(Genders)))
            {
                if (GetDescription<Genders>(c) == description.ToUpper())
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the grade item associated to the specified description.
        /// </summary>
        /// <param name="description">Grade description</param>
        /// <returns>The enum item if the specified description exists, null otherwise</returns>
        public static GradeGroups? GetGradeGroupFromDescription(string description)
        {
            foreach (GradeGroups c in Enum.GetValues(typeof(GradeGroups)))
            {
                if (GetDescription<GradeGroups>(c) == description.ToUpper())
                {
                    return c;
                }
            }
            return null;
        }

        /// <summary>
        /// Get the gradegroup of the specified grade.
        /// </summary>
        /// <param name="grade">Grade code</param>
        /// <returns>The enum item if the specified grade code is correct, null otherwise</returns>
        public static GradeGroups? GetGradeGroupFromGrade(string grade)
        {
            if (grade.Length == 3)
            {
                try
                {
                    int code1 = int.Parse(grade.Substring(0, 2));
                    string code2 = grade.Substring(2, 1);
                    switch(code2)
                    {
                        case "K":
                            if (code1 <= 10 && code1 >= 9)
                                return GradeGroups.KUP_10_9;
                            else if (code1 <= 8 && code1 >= 7)
                                return GradeGroups.KUP_8_7;
                            else if (code1 <= 6 && code1 >= 5)
                                return GradeGroups.KUP_6_5;
                            else if (code1 <= 4 && code1 >= 3)
                                return GradeGroups.KUP_4_3;
                            else if (code1 <= 2 && code1 >= 1)
                                return GradeGroups.KUP_2_1;
                            else
                                return null;
                        case "P":
                            return GradeGroups.POOM;
                        case "D":
                            return GradeGroups.DAN;
                        default:
                            return null;
                    }
                }
                catch(Exception)
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns categories.
        /// </summary>
        /// <returns>Categories</returns>
        public static List<string> GetCategoriesDescriptions()
        {
            List<string> descriptions = new List<string>();
            foreach (Categories c in Enum.GetValues(typeof(Categories)))
                descriptions.Add(GetDescription<Categories>(c));
            return descriptions;
        }

        /// <summary>
        /// Returns genders.
        /// </summary>
        /// <returns>Categories</returns>
        public static List<string> GetGendersDescriptions()
        {
            List<string> descriptions = new List<string>();
            foreach (Genders c in Enum.GetValues(typeof(Genders)))
                descriptions.Add(GetDescription<Genders>(c));
            return descriptions;
        }

        /// <summary>
        /// Returns grades.
        /// </summary>
        /// <returns>Categories</returns>
        public static List<string> GetGradeDescriptions()
        {
            List<string> descriptions = new List<string>();
            foreach (GradeGroups c in Enum.GetValues(typeof(GradeGroups)))
                descriptions.Add(GetDescription<GradeGroups>(c));
            return descriptions;
        }

        /// <summary>
        /// Increments Athlete Round.
        /// </summary>
        public void IncrementRound()
        {
            Round++;
        }
    }
}