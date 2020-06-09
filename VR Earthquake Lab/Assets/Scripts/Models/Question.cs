using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A model class for recording the questions that the user answers.
 */
[Serializable]
public class Question {

    public string ObjectName;
    public bool IsCorrect;

    public Question(string name)
    {
        this.ObjectName = name;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (this.GetType() != obj.GetType()) return false;

        Question p = (Question)obj;
        return (this.ObjectName == p.ObjectName) && (this.IsCorrect == p.IsCorrect);
    }
}
