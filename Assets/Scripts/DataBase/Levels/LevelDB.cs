using System;

public class LevelDB : SQLiteController
{

    private string tableName = "level";

    private String[][] lvlParams = new String[][] {
        //victims
        new String[] {"3"},
        new String[] {"3"},
        new String[] {"3"},
        new String[] {"3"},
        new String[] {"3"}
    };

    public LevelDB(int victims = 0, string leaderboard = "")
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

    public void createStatusLevel(int level, int best_time, int best_stars, int profile)
    {
        int victims;
        if (level - 1 >= 0 && level - 1 < lvlParams.Length && lvlParams[level - 1] != null)
        {
            victims = Int32.Parse(lvlParams[level - 1][0]);
        }
        else
        {
            victims = 0;
        }
        string sql = "INSERT INTO " + tableName + " (level, victims, best_time, best_stars, profile) VALUES (" + level + ", " + victims + "," + best_time + "," + best_stars + "," + profile + ");";
        db.ExecuteNonQuery(sql);
    }

    public void updateStatusLevel(int level, int best_time, int best_stars, int profile)
    {
        string sql = "UPDATE  " + tableName + " SET level = " + level + ", best_time = " + best_time + ", best_stars = " + best_stars + ", profile = " + profile + " WHERE level = " + level + " AND profile = " + profile + ";";
        db.ExecuteNonQuery(sql);
    }

    public string[] getBestStatusLevel(int level, int profile)
    {
        string[] data = new string[1];

        string sql = "SELECT l.best_time FROM level l JOIN profile p ON (l.profile = p.id ) WHERE level = " + level + " AND profile = " + profile + " AND best_time > 0 ORDER BY best_time LIMIT 1;";
        SQLiteDatabase.DBReader response = db.ExecuteReader(sql);
        
        if (response != null && response.Read())
        {
            data[0] = response.GetStringValue("best_time");
        }
        
        return data;
    }

    public int[] getStatusLevel(int level, int profile)
    {
        int[] data = new int[6];

        string sql = "SELECT id, level, active, best_time, best_stars, profile FROM " + tableName + " WHERE level = " + level + " AND profile = " + profile + ";";
        SQLiteDatabase.DBReader response = db.ExecuteReader(sql);
        
        if (response != null && response.Read())
        {
            data[0] = response.GetIntValue("id");
            data[1] = response.GetIntValue("level");
            data[2] = response.GetIntValue("active");
            data[3] = response.GetIntValue("best_time");
            data[4] = response.GetIntValue("best_stars");
            data[5] = response.GetIntValue("profile");
        }
        else
        {
            createStatusLevel(level, 0, 0, profile);
            return getStatusLevel(level, profile);
        }

        return data;
    }

    public int getTotalKills(string codeUser)
    {

        int total = 0;
        string sql = "SELECT SUM(3) as total FROM profile p JOIN level l ON (p.id = l.profile AND l.best_stars > 0) WHERE code = '" + codeUser + "'";
        SQLiteDatabase.DBReader response = db.ExecuteReader(sql);

        if (response != null && response.Read())
        {
            total = response.GetIntValue("total");
        }

        return total;
    }

    private string getScheme()
    {
        return "CREATE TABLE " + tableName + " (" +
            "id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE," +
            "level INTEGER NOT NULL," +
            "active INTEGER NOT NULL DEFAULT 0," +
            "victims INTEGER," +
            "best_time INTEGER NOT NULL DEFAULT 0," +
            "best_stars INTEGER NOT NULL DEFAULT 0," +
            "profile INTEGER NOT NULL," +
            "FOREIGN KEY (profile) REFERENCES profile(id) ON DELETE CASCADE" +
            ");";
    }
}
