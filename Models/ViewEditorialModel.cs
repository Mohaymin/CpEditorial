﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class ViewEditorialModel
    {
        DbSchema dSchema = new DbSchema();

        public int voteStatus { get; set; } // 0 = no vote,  1 = upvote,  2 = downvote
        public Editorial editorial { get; set; }
        public Problem problem { get; set; }
        public EditorialTags editorialTags { get; set; }
        public User user { get; set; }
        public OnlineJudge onlineJudge { get; set; }
        public Comment comment { get; set; }
        public List<Comment> commentList { get; set; }
        public List<List<Comment>> replyList { get; set; }
        public ViewEditorialModel(int editorialID)
        {
            Initialize(editorialID);
            voteStatus = 0;
        }

        public ViewEditorialModel(int editorialID, int userID)
        {
            Initialize(editorialID);
            string query = "select votetype from votetrack where userid = " + userID + " and editorialid = " + editorialID;
            DataTable dTable = new DBHelper().getTable(query);
            if (dTable.Rows.Count == 0)
            {
                voteStatus = 0;
            }
            else
            {
                if (Convert.ToString(dTable.Rows[0][0]) == "upvote")
                {
                    voteStatus = 1;
                }
                else
                {
                    voteStatus = 2;
                }
            }
        }

        void Initialize(int editorialID)
        {
            editorial = dSchema.GetEditorial(editorialID);
            problem = dSchema.GetProblem(editorial.problemId);
            editorialTags = dSchema.GetEditorialTags(editorial.editorialId);
            user = dSchema.GetUser(editorial.userId);
            onlineJudge = dSchema.GetOnlineJudge(problem.ojId);
            //comment = new Comment();

            commentList = dSchema.GetCommentsOfEditorial(editorialID);
            replyList = new List<List<Comment>>();
            foreach (Comment c in commentList)
            {
                try
                {
                    replyList.Add(dSchema.GetRepliesOfComment(c.commentId));
                }
                catch (System.NullReferenceException)
                {
                    replyList.Add(new List<Comment>());
                }
            }
        }
    }
    //public class ViewEditorialModel : EditorialModel
    //{
    //    DataTable ediTbl = new DataTable();
    //    public ViewEditorialModel(int editorialID)
    //    {
    //        string sql = "select * from Editorial, [User], Tag, Problem, OnlineJudge where Editorial.EditorialID=" + editorialID + " and Editorial.UserID=[User].UserID and Editorial.TagID=Tag.TagID and Editorial.ProblemID=Problem.ProblemID and Problem.OJID=OnlineJudge.OJID";
    //        ediTbl = new DBHelper().getTable(sql);

    //        ProblemTitle = Convert.ToString(ediTbl.Rows[0][20]);
    //        Rephrase = Convert.ToString(ediTbl.Rows[0][4]);
    //        Solution = Convert.ToString(ediTbl.Rows[0][5]);
    //        Details = Convert.ToString(ediTbl.Rows[0][6]);
    //        TagName = Convert.ToString(ediTbl.Rows[0][17]);
    //        UserName = Convert.ToString(ediTbl.Rows[0][11]);
    //        TagID = Convert.ToInt32(ediTbl.Rows[0][3]);
    //        DateOfPublishing = Convert.ToString(ediTbl.Rows[0][9]);
    //    }
    //}
}
