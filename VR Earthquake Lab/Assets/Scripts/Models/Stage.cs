using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A model class for recording the stage information in each scenario. 
 */
[Serializable]
public class Stage {

    public int StageNumber;
    public string StageGoal;
    public List<Question> questions = new List<Question>();


    public Stage(int stageNum, string stageGoal)
    {
        this.StageNumber = stageNum;
        this.StageGoal = stageGoal;
    }
    public void AddQuestion(Question question)
    {
        if (ContainsQuestion(question))
        {
            questions.Remove(question);
            return;
        }
        
        questions.Add(question);
    }

    private bool ContainsQuestion(Question question)
    {
        foreach(Question q in questions)
        {
            if (question.Equals(q))
            {
                return true;
            }
        }

        return false;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (this.GetType() != obj.GetType()) return false;

        Stage p = (Stage)obj;
        return (this.StageNumber == p.StageNumber) && (this.StageGoal == p.StageGoal);
    }
}
