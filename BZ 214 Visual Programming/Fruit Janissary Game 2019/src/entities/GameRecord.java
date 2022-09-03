package entities;

import java.sql.Date;

public class GameRecord {

    private Integer id;
    private Integer user_id;
    private String username;
    private Date date;
    private float duration;
    private Integer point;

    public GameRecord(){

    }

    public GameRecord(Integer user_id, Date date, float duration, Integer point) {
        this.user_id = user_id;
        this.date = date;
        this.duration = duration;
        this.point = point;
    }

    public GameRecord(Integer id, String username, Date date, float duration, Integer point) {
        this.id = id;
        this.username = username;
        this.date = date;
        this.duration = duration;
        this.point = point;
    }

    public Integer getUser_id() {
        return user_id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public float getDuration() {
        return duration;
    }

    public void setDuration(float duration) {
        this.duration = duration;
    }

    public Integer getPoint() {
        return point;
    }

    public void setPoint(Integer point) {
        this.point = point;
    }

}
