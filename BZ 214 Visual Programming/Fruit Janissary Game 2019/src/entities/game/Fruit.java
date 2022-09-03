package entities.game;

import javafx.scene.image.Image;
import javafx.scene.image.ImageView;
import javafx.scene.layout.Pane;
import javafx.scene.shape.Polygon;

import java.util.HashMap;

public abstract class Fruit implements Sliceable {

    public String name;
    public int point;

    public ImageView fruitImgView;
    public ImageView partLeft;
    public ImageView partRight;

    public ImageView splash;

    public double height;
    public double width;

    public boolean isCut = false;

    public abstract void create();

    public abstract void animateSlice(Pane gameCanvas, double cutPosX, double cutPosY);
    public abstract void animateSplash(Pane gameCanvas, double cutPosX, double cutPosY);

}

