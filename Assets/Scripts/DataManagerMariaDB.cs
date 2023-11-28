using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using MySql.Data;
using MySql.Data.MySqlClient;
using TMPro;
using System;

public class DataManagerMariaDB : MonoBehaviour
{
    // ---------------- Variables
    public string myLoginId;
    public bool isLogined;
    public GameObject dataManager;

    public InputField inputUserName;
    public InputField inputPassword;
    public InputField inputEmail;
    public TMP_Text displayMessage;

    string inputStrUserName;
    string inputStrPassword;
    string inputStrEmail;

    // For using database (mariaDB)
    private MySqlConnection myConnection = null;
    private MySqlCommand myCommand = null;
    private MySqlDataReader myDataReader = null;


    // --------------- Methods
    // Start is called before the first frame update
    void Start()
    {
        SetupSQLConnection();

        /* 
         * private 에서 public 으로 접근권한자를 변경
        dataManager = GameObject.Find("DataManager");
        if (dataManager == null) 
        {
            Debug.Log("DataManager is NOT found");
            displayMessage.text = "DataManager is NOT found";
        }
        else
        {
            Debug.Log("DataManager is found ...");
            displayMessage.text = "DataManager is found ...";
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SignUp()  // Signup button is clicked : 회원가입 - INSERT문 실행
    {
        //Debug.Log("SIGNUP:" + inputStrUserName + " " + inputStrPassword + " " + inputStrEmail);
        displayMessage.text = "SIGNUP:" + inputStrUserName + " " + inputStrPassword + " " + inputStrEmail;
        
        SignUpMariaDB();
    }

    public void Login() // Login button is clicked : 로그인 - SELECT
    {
        //Debug.Log("LOGIN:" + inputStrUserName + " " + inputStrPassword);
        displayMessage.text = "LOGIN:" + inputStrUserName + " " + inputStrPassword;

        LoginMariaDB();
    }

    public void ChangedInputUserName(Text changedString)
    {
        inputStrUserName = changedString.text;
    }

    public void ChangedInputPassword(Text changedString)
    {
        inputStrPassword = changedString.text;
    }

    public void ChangedInputEmail(Text changedString)
    {
        inputStrEmail = changedString.text;
    }

    // -------------------------- Database
    private void SetupSQLConnection()
    {
        if (myConnection == null)
        {
            string connectionString =
                "SERVER=61.245.246.242; Port=3306; " +
                "DATABASE=wonkwangdc; " +
                "UID=wonkwangdc; " +
                "PASSWORD=wkdc2017!A;";
            try
            {
                myConnection = new MySqlConnection(connectionString);
                //connection.Open();
                //Debug.Log("[MariaDB Connection] Connected successfully ..");
                displayMessage.text = "[MariaDB Connection] Connected successfully ..";
            }
            catch (MySqlException ex)
            {
                //Debug.LogError("[MariaDB Connection Error] " + ex.ToString());
                displayMessage.text = "[MariaDB Connection Error] " + ex.ToString();
            }
        }
    }

    private bool CheckDataForMariaDB(int p)
    {
        if (p == 1) // Signup 일때
        {
            //Debug.Log("Signup [" + inputStrUserName + " " + inputStrPassword + " " + inputStrEmail + "]");
            if ((inputStrUserName != null) && (inputStrPassword != null) && (inputStrEmail != null) &&
                (inputStrUserName != "") && (inputStrPassword != "") && (inputStrEmail != ""))
            {
                return true;
            }
            else
            {
                //Debug.Log("[INPUT ERROR] User input has an ERROR (NOT Enough to signup)");
                displayMessage.text = "[INPUT ERROR] User input has an ERROR (NOT Enough to signup)";
                return false;
            }
        }
        else if (p == 2) // Login 일때
        {
            //Debug.Log("Login [" + inputStrUserName + " " + inputStrPassword + "]");
            if ((inputStrUserName != null) && (inputStrPassword != null) &&
                (inputStrUserName != "") && (inputStrPassword != ""))
            {
                return true;
            }
            else
            {
                //Debug.Log("[INPUT ERROR] User login input has an ERROR (NOT Enough to login)");
                displayMessage.text = "[INPUT ERROR] User login input has an ERROR (NOT Enough to login)";
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    // --------------------------------- SIGNUP
    private void SignUpMariaDB()
    {
        if (CheckDataForMariaDB(1))
        {
            //Debug.Log("Signup is continued ..");
            //RegisterUserToMariaDBPlayer();

            if (InsertUsernameToMariaDB())  // INSERT문을 실행해서 성공하면
            {
                inputUserName.text = "";
                inputPassword.text = "";
                inputEmail.text = "";

                //Debug.Log("OKed. Signup is completed successfully");
                displayMessage.text = "OKed. Signup is completed successfully";
            }
        }
        else
        {
            //Debug.LogError("[ERROR] Error is occurred during your register ..");
            displayMessage.text = "[ERROR] Error is occurred during your register ..";
        }
    }

    private bool InsertUsernameToMariaDB()
    {
        string currentDateTimeValue = "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        string usernameValue = "'" + inputStrUserName + "'";
        string passwordValue = "'" + inputStrPassword + "'";
        string emailValue = "'" + inputStrEmail + "'";

        string commandText = string.Format(
            "INSERT INTO player " +
            "(username, password, email, confirmationdate) " +
            "VALUES ({0}, {1}, {2}, {3})",
            usernameValue, passwordValue, emailValue, currentDateTimeValue
        );
        //Debug.Log(commandText);

        if (myConnection != null)
        {
            myCommand = myConnection.CreateCommand();
            myCommand.CommandText = commandText;
            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                //Debug.LogError("[MariaDB SQL Error] " + ex.ToString());
                displayMessage.text = "[MariaDB SQL Error] " + ex.ToString();
                return false;
            }
        }
        else
        {
            //Debug.LogError("[MariaDB Error] Connection error ..");
            displayMessage.text = "[MariaDB Error] Connection error ..";
            return false;
        }
    }

    //----------------------------- LOGIN
    private void LoginMariaDB()
    {
        if (CheckDataForMariaDB(2))
        {
            //Debug.Log("Login is continued ..");

            int res = SelectUsernameFromMariaDB();
            if (res > 0)
            {
                // 로그인에 성공하면
                // (1)UserID를 씬에서 보관
                // (2)Login_History 테이블에 로그인 기록 저장
                //GameObject.Find("GameManager").GetComponent<GameManager>().myGlobalPlayFabId = res.ToString();
                dataManager.GetComponent<DataManagerMariaDB>().myLoginId = res.ToString();
                dataManager.GetComponent<DataManagerMariaDB>().isLogined = true;
                
                RecordLoginHistoryToMariaDB(res);
            }
            else
            {
                //Debug.Log("[" + res + "] Login failed ...");
                displayMessage.text = "[" + res + "] Login failed ... NO login information";
                inputUserName.text = "";
                inputPassword.text = "";
            }
        }
        else
        {
            //Debug.LogError("[ERROR] Error is occurred during your login ..");
            displayMessage.text = "[ERROR] Error is occurred during your login ..";
        }
    }

    //private DataTable LoginToMariaDBPlayer()
    private int SelectUsernameFromMariaDB()
    {
        //DataTable dt = new DataTable();

        string commandTextSelect = string.Format(
            "SELECT * FROM player WHERE username='" + inputStrUserName + "' AND password='" + inputStrPassword + "'"
        );
        //Debug.Log(commandTextSelect);

        myConnection.Open();

        //MySqlDataAdapter adapter = new MySqlDataAdapter(commandTextSelect, myConnection);
        //adapter.Fill(dt);
        myCommand = new MySqlCommand(commandTextSelect, myConnection);
        myDataReader = myCommand.ExecuteReader();

        int playerID = -1;
        while (myDataReader.Read())
        {
            //Debug.Log(myDataReader[0] + "-" + myDataReader[1] + "-" + myDataReader[3] + "-" + myDataReader[6]);
            displayMessage.text = myDataReader[0] + "-" + myDataReader[1] + "-" + myDataReader[3] + "-" + myDataReader[6];
            playerID = (int)myDataReader[0];
        }

        myConnection.Close();

        //return dt;
        return playerID;
    }

    private void RecordLoginHistoryToMariaDB(int id)
    {
        string currentDateTimeValue = "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        string usernameValue = "'" + inputStrUserName + "'";

        string commandText = string.Format(
            "INSERT INTO login_history (player_id, login_time) " +
            "VALUES ({0}, {1})",
            id, currentDateTimeValue
        );

        Debug.Log(commandText);

        if (myConnection != null)
        {
            myCommand = myConnection.CreateCommand();
            myCommand.CommandText = commandText;
            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                myConnection.Close();

                StartGame();
            }
            catch (System.Exception ex)
            {
                //Debug.LogError("[MariaDB SQL Error] " + ex.ToString());
                displayMessage.text = "[MariaDB SQL Error] " + ex.ToString();
            }
        }
        else
        {
            //Debug.LogError("[MariaDB Error] Connection error ..");
            displayMessage.text = "[MariaDB Error] Connection error ..";
        }
    }

    //------------------- Game Start !
    void StartGame()
    {
        //Debug.Log("Now, start the game, enjoy it");
        displayMessage.text = "Now, start the game, enjoy it";
    }

}
