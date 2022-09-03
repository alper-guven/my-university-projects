package models;

import config.DatabaseConnection;
import entities.GameRecord;
import entities.User;

import java.sql.*;
import java.util.HashSet;
import java.util.Set;

public class GameRecordDao {

    public void insertGameRecord(GameRecord gameRecord) {

        Connection connection = DatabaseConnection.getConnection();
        try {
            PreparedStatement ps = connection.prepareStatement("INSERT INTO games VALUES (NULL, ?, ?, ?, ?)");
            ps.setInt(1, gameRecord.getUser_id());
            ps.setDate(2, gameRecord.getDate());
            ps.setFloat(3, gameRecord.getDuration());
            ps.setInt(4, gameRecord.getPoint());

            int i = ps.executeUpdate();
//            if(i == 1) {
////                return true;
//            }
        } catch (SQLException ex) {
            ex.printStackTrace();
        } finally {
            try {
                connection.close();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }

//        return false;
    }


    public Set getPreviousGamesOfUser(User user) {
        Connection connection = DatabaseConnection.getConnection();
        try {
            PreparedStatement ps = connection.prepareStatement("SELECT * FROM games INNER JOIN users ON games.user_id=users.id WHERE user_id = ? ORDER BY date DESC");
            ps.setInt( 1, user.getId() );

            ResultSet rs = ps.executeQuery();

            Set gameRecords = new HashSet();
            while(rs.next())
            {
                GameRecord gameRecord = extractGameRecordFromRS(rs);
                gameRecords.add(gameRecord);
            }
            return gameRecords;
        } catch (SQLException ex) {
            ex.printStackTrace();
        }
        return null;
    }

    public Set getLeaderboard() {
        Connection connection = DatabaseConnection.getConnection();
        try {
            Statement ps = connection.createStatement();

            ResultSet rs = ps.executeQuery("SELECT * FROM games INNER JOIN users ON games.user_id=users.id ORDER BY point DESC, duration LIMIT 10");

            Set gameRecords = new HashSet();
            while(rs.next())
            {
                GameRecord gameRecord = extractGameRecordFromRS(rs);
                gameRecords.add(gameRecord);
            }
            return gameRecords;
        } catch (SQLException ex) {
            ex.printStackTrace();
        }
        return null;
    }

    private GameRecord extractGameRecordFromRS(ResultSet rs) throws SQLException {
        GameRecord gameRecord = new GameRecord();
        gameRecord.setId( rs.getInt("id") );
        gameRecord.setUsername( rs.getString("username") );
        gameRecord.setDate( rs.getDate("date"));
        gameRecord.setDuration( rs.getFloat("duration"));
        gameRecord.setPoint( rs.getInt("point"));

        return gameRecord;
    }

}
