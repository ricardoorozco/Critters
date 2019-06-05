using UnityEngine;
using SQLiteDatabase;

public class SQLiteController
{

    protected SQLiteDB db = SQLiteDB.Instance;

    // Events
    void OnEnable()
    {
        SQLiteEventListener.onError += OnError;
    }

    void OnDisable()
    {
        SQLiteEventListener.onError -= OnError;
    }

    void OnError(string err)
    {
        Debug.Log(err);
    }

    // Use this for initialization
    public SQLiteController()
    {
        // set database location (directory)
        db.DBLocation = Application.persistentDataPath;
        db.DBName = "critters.db";

        if (db.Exists)
        {
            ConnectToDB();
        }
        else
        {
            CreateDB();
        }
    }

    // create database and table
    void CreateDB()
    {
        // create database at specified location
        db.CreateDatabase (db.DBName,true);
    }

    void ConnectToDB()
    {
        // connect database at specified location
        db.CreateDatabase (db.DBName,false);
    }

    // use this to avoid any lock on database, otherwise restart editor or application after each run
    void OnApplicationQuit()
    {
        // release all resource using by database.
        db.Dispose();
    }
}
