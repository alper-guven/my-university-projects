package entities.game;

import javafx.scene.image.ImageView;
import javafx.scene.layout.Pane;




public abstract class Bomb implements Sliceable {

    public ImageView bombImageView;
    public double width;
    public double height;

    public ImageView bombExplotionImg;

    public abstract void explode(Pane gameCanvas, double cutPosX, double cutPosY);
    public abstract void create();
}
