import controllers.Game;
import entities.GameRecord;
import entities.User;
import javafx.application.Application;
import javafx.collections.FXCollections;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXMLLoader;
import javafx.geometry.Rectangle2D;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.*;
import javafx.scene.paint.Color;
import javafx.stage.Screen;
import javafx.stage.Stage;
import javafx.scene.image.Image;
import models.UserDao;


public class Main extends Application {

    private Stage launcher;
    private Scene loginScene;
//    private Scene registerScene;

    private Stage gameWindow;
    private Scene gameLobbyScene;

    private User loggedUser;

    @Override
    public void start(Stage primaryStage) throws Exception {

        launcher = primaryStage;

        openLauncher();

    }

    private void openLauncher(){

        Pane canvas = new Pane();

        BackgroundImage myBI= new BackgroundImage(new Image("images/fruitjan-bg.png",1000,500,false,true),
                BackgroundRepeat.REPEAT, BackgroundRepeat.NO_REPEAT, BackgroundPosition.DEFAULT,
                BackgroundSize.DEFAULT);
        canvas.setBackground(new Background(myBI));

        int rightStartX = 700;

        Label lblLogin = new Label("Login");
        lblLogin.relocate(rightStartX, 110);

        TextField txtUsername = new TextField();
        txtUsername.relocate(rightStartX, 140);

        TextField txtPassword = new TextField();
        txtPassword.relocate(rightStartX, 180);

        Button btnLogin = new Button("Login");
        btnLogin.relocate(rightStartX, 220);

        UserDao userDao = new UserDao();

        btnLogin.setOnAction( e -> {

            User loginResponse = null;

            if( txtUsername.getText() != null && !txtUsername.getText().isEmpty() && txtPassword.getText() != null && !txtPassword.getText().isEmpty() ){
                loginResponse = userDao.getUserByUserNameAndPassword( txtUsername.getText(), txtPassword.getText() );
            }


           if(loginResponse != null ){
               loggedUser = loginResponse;

               launcher.hide();

               openGameWindow();
           }

        });

        Label lblRegister = new Label("Need an account?");
        lblRegister.relocate(rightStartX, 280);

        Button btnRegister = new Button("Register");
        btnRegister.relocate(rightStartX, 220);
        btnRegister.setOnAction(e->{
            boolean isRegisterSuccess = false;

            if( txtUsername.getText() != null && !txtUsername.getText().isEmpty() && txtPassword.getText() != null && !txtPassword.getText().isEmpty() ){
                isRegisterSuccess = userDao.insertUser( new User(txtUsername.getText(), txtPassword.getText()) );
            }

        });

        var wrapper = new Object(){
            public Button btnNeedRegister;
        };

        Button btnBackToLogin = new Button("Back to Login");
        btnBackToLogin.relocate(rightStartX, 280);
        btnBackToLogin.setOnAction(e->{
            lblLogin.setText("Login");
            canvas.getChildren().removeAll(btnRegister, btnBackToLogin);
            canvas.getChildren().addAll(lblRegister, wrapper.btnNeedRegister);
        });

        wrapper.btnNeedRegister = new Button("Register");
        wrapper.btnNeedRegister.relocate(rightStartX, 310);
        wrapper.btnNeedRegister.setOnAction(e->{
            lblLogin.setText("Register");
            canvas.getChildren().removeAll(lblRegister, wrapper.btnNeedRegister);
            canvas.getChildren().addAll(btnRegister, btnBackToLogin);
        });



        canvas.getChildren().addAll( lblLogin, btnLogin, txtUsername, txtPassword, lblRegister, wrapper.btnNeedRegister );

        loginScene = new Scene (canvas , 1000 , 500);

        launcher.setTitle("Fruit Janissary - Launcher");
        launcher.setScene(loginScene);
        launcher.setResizable(false);
        launcher.show();

//        UserDao userDao = new UserDao();
//        User newUser = new User("alper", "12345");
//        userDao.insertUser(newUser);

    }

    private void switchLauncherScene(Scene switchedScene){

        launcher.setScene(switchedScene);

    }

    private void openGameWindow(){

        gameWindow = new Stage();

        Rectangle2D primaryScreenBounds = Screen.getPrimary().getVisualBounds();

        //set Stage boundaries to visible bounds of the main screen
        gameWindow.setX(primaryScreenBounds.getMinX());
        gameWindow.setY(primaryScreenBounds.getMinY());
        gameWindow.setWidth(primaryScreenBounds.getWidth());
        gameWindow.setHeight(primaryScreenBounds.getHeight());


        Pane lobbyCanvas = new Pane();

        Button btnPlay = new Button("Play");
        btnPlay.resize(100,100);
        btnPlay.relocate(200,200);

        btnPlay.setOnAction( e -> {

            Game newGame = new Game(gameWindow, gameLobbyScene, lobbyCanvas, loggedUser);
            newGame.play();

//            gameWindow.setScene(gameLobbyScene);

        });

        lobbyCanvas.getChildren().addAll(btnPlay);

        gameLobbyScene = new Scene(lobbyCanvas, primaryScreenBounds.getWidth(),  primaryScreenBounds.getHeight());

        gameWindow.setScene(gameLobbyScene);
        gameWindow.setTitle("Fruit Janissary");
        gameWindow.setResizable(false);
        gameWindow.setFullScreen(true);
        gameWindow.show();



    }


}