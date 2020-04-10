using System;
using System.Collections.Generic;
using UnityEngine;

public class ProfileDB : SQLiteController
{

    private string tableName = "profile";

    List<String[]> profilesList = new List<String[]>();

    private int id;
    private string username;
    private string creationDate;

    public ProfileDB()
    {
        if (!db.IsTableExists(tableName))
        {
            createTable();
        }
	}

	public string getTableName()
	{
		return tableName;
	}

    private void createTable()
    {
		db.ExecuteNonQuery(getScheme());
    }

	public void createProfile(string code, string username)
    {
		string sql = "INSERT INTO " + tableName + " (code, username, creation_date) VALUES ('" + code + "', '" + username + "', '" + DateTime.Now + "');";
		db.ExecuteNonQuery(sql);
    }

    public List<String[]> listProfiles()
    {
		SQLiteDatabase.DBReader profiles = db.GetAllData(tableName);

        profilesList.Clear();

        while (profiles != null && profiles.Read())
        {
            String[] dataProfile = new String[3];
            dataProfile[0] = profiles.GetIntValue("id").ToString();
            dataProfile[1] = profiles.GetStringValue("username");
            dataProfile[2] = profiles.GetStringValue("creation_date");

            profilesList.Add(dataProfile);
        }

        return profilesList;
	}

	public void setCurrentUser(string username) {
		string sqlResetCurrentUser = "UPDATE " + tableName + " SET current_user = 0;";
		db.ExecuteNonQuery(sqlResetCurrentUser);
		string sqlSetCurrentUser = "UPDATE " + tableName + " SET current_user = 1 WHERE username = '" + username + "';";
		db.ExecuteNonQuery(sqlSetCurrentUser);

	}

	public string getCurrentUserName() {

		string username = "";
		string sql = "SELECT username FROM " + tableName + " WHERE current_user = 1;";
		SQLiteDatabase.DBReader response = db.ExecuteReader(sql);

		if (response != null && response.Read ()) {
			username = response.GetStringValue("username");
		}

		return username;
	}

	public int getCurrentUserId() {

		int id = 0;
		string sql = "SELECT id FROM " + tableName + " WHERE current_user = 1;";
		SQLiteDatabase.DBReader response = db.ExecuteReader(sql);

		if (response != null && response.Read ()) {
			id = response.GetIntValue("id");
		}

		return id;
	}

	public string getUserNameByCode(string code) {

		string username = null;
		string sql = "SELECT username FROM " + tableName + " WHERE code = '" + code + "';";
		SQLiteDatabase.DBReader response = db.ExecuteReader(sql);

		if (response != null && response.Read ()) {
			username = response.GetStringValue("username");
		}

		return username;
	}

	private string getScheme(){
		return "CREATE TABLE " + tableName + " (" +
			"id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
			"code TEXT NOT NULL UNIQUE," +
			"username TEXT NOT NULL UNIQUE," +
			"current_user INTEGER NOT NULL DEFAULT 0," +
            "creation_date TEXT NOT NULL" +
			");";
	}
}
