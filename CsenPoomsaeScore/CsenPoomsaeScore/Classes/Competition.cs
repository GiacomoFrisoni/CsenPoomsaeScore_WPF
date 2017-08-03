using System.Collections.Generic;
using System.Linq;

namespace CsenPomsaeScore
{
    /// <summary>
    /// This class handles a Competition and uses Singleton pattern.
    /// </summary>
    public class Competition
    {
        private static Competition competition;

        private string name;
        private int judgeNumber;
        private List<Athlete> athletes = new List<Athlete>();

        private Competition() { }

        public static Competition MyCompetition
        {
            get
            {
                if (competition == null)
                {
                    competition = new Competition();
                }
                return competition;
            }
        }

        
        /// <summary>
        /// Return the next Athlete ID to use.
        /// </summary>
        /// <returns>Next Athlete ID</returns>
        public int GetNextAthleteCounter()
        {
            return athletes.Count + 1;
        }
        
        /// <summary>
        /// Set the name of the competition.
        /// </summary>
        /// <param name="name">Competition name</param>
        public void SetCompetitionName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Set the number of judges.
        /// </summary>
        /// <param name="number">Judges number</param>
        public void SetJudgeNumber(int number)
        {
            this.judgeNumber = number;
        }

        /// <summary>
        /// Returns the number of judges.
        /// </summary>
        /// <returns>Judges number</returns>
        public int GetJudgeNumber()
        {
            return this.judgeNumber;
        }
        
        /// <summary>
        /// Add a new athlete.
        /// </summary>
        /// <param name="athlete">An "Athlete" object ready to use</param>
        /// <returns>TRUE if added succesfuly, FALSE otherwise</returns>
        public void AddAthlete(Athlete athlete)
        {
            athletes.Add(athlete);
        }

        /// <summary>
        /// Find an athlete with a specific ID.
        /// </summary>
        /// <param name="ID">ID of the Athlete</param>
        /// <returns>An "Athlete" object ready to use</returns>
        public Athlete GetAthlete(int ID)
        {
            if (ID > 0 && ID < athletes.Count)
                return athletes[ID];
            else
                return null;
        }

        /// <summary>
        /// Returns all the athletes.
        /// </summary>
        /// <returns>List of all athletes</returns>
        public List<Athlete> GetAthletes()
        {
            return athletes;
        }

        /// <summary>
        /// Returns all the athletes with the specified params.
        /// </summary>
        /// <param name="category">Category</param>
        /// <param name="gender">Gender</param>
        /// <param name="grade">Grade Group</param>
        /// <returns></returns>
        public List<Athlete> GetFilteredAthlete(Athlete.Categories category, Athlete.Genders gender, Athlete.GradeGroups gradeGroup)
        {
            return athletes.Where(a => a.Category == category && a.Gender == gender && a.GradeGroup == gradeGroup).OrderBy(a => a.Name).ThenBy(a => a.Surname).ToList();
        }


        /// <summary>
        /// Class used to store tmp linq results. 
        /// </summary>
        private class SingleGroup
        {
            public Athlete.Categories Category { get; set; }
            public Athlete.Genders Gender { get; set; }
            public Athlete.GradeGroups GradeGroup { get; set; }
        }
        
        /// <summary>
        /// Returns the athletes collocated into single groups.
        /// </summary>
        /// <returns>Single athletes</returns>
        public List<Athlete> GetSingleAthletes()
        {
            List<Athlete> singleAthletes = new List<Athlete>();
            List<SingleGroup> singleGroups = athletes
                .GroupBy(a => new SingleGroup { Category = a.Category, Gender = a.Gender, GradeGroup = a.GradeGroup })
                .Where(g => g.Count() == 1)
                .Select(g => g.Key)
                .ToList();
            foreach (Athlete a in athletes)
                foreach (SingleGroup g in singleGroups)
                    if (a.Category == g.Category && a.Gender == g.Gender && a.GradeGroup == g.GradeGroup)
                        singleAthletes.Add(a);
            return singleAthletes;
        }

        /// <summary>
        /// Remove an athlete from the category
        /// </summary>
        /// <param name="athlete">An "Athlete" object to remove</param>
        /// <returns>TRUE if object is removed, FALSE otherwise</returns>
        public bool RemoveAthlete(Athlete athlete)
        {
            return athletes.Remove(athlete);
        }
        
        
        /// <summary>
        /// Destroy all athletes and categories. Clear all.
        /// </summary>
        public void ClearAll()
        {
            athletes.Clear();
        }
    }
}