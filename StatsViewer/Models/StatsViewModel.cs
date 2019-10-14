using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsXmlProcessor.Models;
using static System.Math;

namespace StatsViewer.Models
{
    public class StatsViewModel
    {
        public DateTime ProcessDate { get; internal set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; internal set; }
        public int AccountId { get; set; }
        public int ProfileViews { get; internal set; }
        public string AboutMe { get; internal set; }
        public DateTime LastAccessDate { get; internal set; }
        public DateTime CreationDate { get; internal set; }
        public int Reputation { get; internal set; }
        public string UserName { get; internal set; }
        public string Location { get; internal set; }

        public double AverageQuestionsPerDay
        {
            get
            {
                if (Questions == 0 || ProcessDate <= CreationDate) return 0;
                return (double) ((ProcessDate - CreationDate).Days) / Questions;
            }
        }

        public double AverageAnswersPerDay
        {
            get
            {
                if (Answers == 0 || ProcessDate <= CreationDate) return 0;
                return (double)((ProcessDate - CreationDate).Days) / Answers;
            }
        }
        public double AveragePostsPerDay
        {
            get
            {
                if (Questions + Answers == 0 || ProcessDate <= CreationDate) return 0;
                return (double)((ProcessDate - CreationDate).Days) / Questions + Answers;
            }
        }
        public string SiteName { get; set; }
        public int SiteId { get; set; }
        public int Questions { get; internal set; }
        public List<int> QuestionIds { get; internal set; }
        public int TotalWordsForQuestions { get; internal set; }
        public int ShortestQuestionWordCount { get; internal set; }
        public int LongestQuestionWordCount { get; internal set; }
        public int TotalTitleWordCount { get; internal set; }
        public int LongestQuestionTitleWordCount { get; internal set; }
        public short HighestScoredQuestion { get; internal set; }
        public short LowestScoredQuestion { get; internal set; }
        public int Answers { get; internal set; }
        public List<int> AnswerIds { get; internal set; }
        public int TotalWordsForAnswers { get; internal set; }
        public int LongestAnswerWordCount { get; internal set; }
        public int ShortestAnswerWordCount { get; internal set; }
        public short HighestScoredAnswer { get; internal set; }
        public short LowestScoredAnswer { get; internal set; }

        public int MedianQuestionScore
        {
            get
            {
                var count = QuestionIds.Count;
                if (count == 0) return 0;
                if (count == 1) return QuestionIds[0];
                if (count % 2 == 0)
                {
                    var middle = count / 2;
                    return (QuestionIds[middle] + QuestionIds[middle - 1]) / 2;
                }

                return QuestionIds[Convert.ToInt32(Math.Floor(Convert.ToDouble(count / 2)))];
            }
        }

        public int MedianAnswerScore 
        {
            get
            {
                var count = AnswerIds.Count;
                if (count == 0) return 0;
                if (count == 1) return AnswerIds[0];
                if (count % 2 == 0)
                {
                    var middle = count / 2;
                    return (AnswerIds[middle] + AnswerIds[middle - 1]) / 2;
                }

                return AnswerIds[Convert.ToInt32(Math.Floor(Convert.ToDouble(count / 2)))];
            }
        }
        public int OwnerUserId { get; set; }
        public List<Badge> Badges { get; set; }
        public List<int> CommentScores { get; set; }

        public double AverageCommentsPerPost
        {
            get
            {
                if (this.Comments == 0)
                {
                    return 0;
                }

                if (Answers + Questions == 0)
                {
                    return 0;
                }

                return (double) Comments / (Answers + Questions);
            }
        }

        public double AverageCommentsPerQuestion
        {
            get
            {
                if (this.Comments == 0 || Questions == 0)
                {
                    return 0;
                }

                return (double) this.Comments / Questions;
            }
        }
        public double AverageCommentsPerAnswer
        {
            get
            {
                if (this.Comments == 0 || Answers == 0)
                {
                    return 0;
                }

                return (double)this.Comments / Answers;
            }
        }

        public double AverageNumberOfBadgesEarnedPerDay
        {
            get
            {
                if (Badges.Count == 0 || CreationDate == DateTime.MinValue || CreationDate > ProcessDate ||
                    ((Questions + Answers == 0)))
                {
                    return 0;
                }

                var days = ProcessDate - CreationDate;
                if (days.Days <= 0)
                {
                    return 0;
                }

                return (double) Badges.Count / days.Days;
            }
        }
        public int Comments { get; internal set; }
        public int ShortestCommentWordCount { get; internal set; }
        public int LongestCommentWordCount { get; internal set; }
        public int TotalCommentWordCount { get; internal set; }
        public int HighestScoredComment { get; internal set; }
        public int LowestScoredComment { get; internal set; }
        public int UserId { get; set; }
    }
}
