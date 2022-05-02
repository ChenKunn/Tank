using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoginManger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LoginCanvas;
    public GameObject RegisterCanvas;
    public GameObject MessageCanvas;
    public InputField InputAccount;
    public InputField InputPassword;
    public Button LoginButton;
    
    public InputField InputUserName;
    public InputField InputAccount1;
    public InputField InputPassWord1;
    public Button RegisterButton;
    public Text UserNameText;
    public Text message;

    public Button StartButton;
    private SqlAccess sql;
    private bool IsLogin;
    void Start()
    {
        //连接数据库
        sql = new SqlAccess();
        IsLogin = false;
        //登录按钮监听
        LoginButton.onClick.AddListener(()=>{
            Login();//登录
        });
        //注册按钮监听
        RegisterButton.onClick.AddListener(()=>{
            Register();
        });

        StartButton.onClick.AddListener(()=>{
            StartGame();
        });
    }

    private void StartGame()
    {
        if(IsLogin == false){
            LoginCanvas.SetActive(true);
        }else
        {
            LoginLoadScene();
        }
    }
    private void Login(){
        string ps = sql.GetItemByNum("password", InputAccount.text);
        string name = sql.GetItemByNum("name", InputAccount.text);
        if(ps != null){
            SaveData.Instance.playerid =sql.GetIdByNum(InputAccount.text);
            UserNameText.text = name;
            IsLogin = true;
            LoginCanvas.SetActive(false);
        }else{
            MessageCanvas.SetActive(true);
            message.text = "用户名或密码不正确！";
            Debug.Log("请输入用户名和密码！");
        }
    }

    private void Register(){
        string username = InputUserName.text;
        string pnum = InputAccount1.text;
        string pw = InputPassWord1.text;
        string exist = sql.GetItemByNum("id", pnum);
        if(IsNull(username) || IsNull(pnum) || IsNull(pw))
        {
            MessageCanvas.SetActive(true);
            message.text = "请确保输入框非空！";
            return;
        }
        if(username.Length > 8)
        {
            MessageCanvas.SetActive(true);
            message.text = "用户名过长！";
            return;
        }
        if(pnum.Length != 11)
        {
            MessageCanvas.SetActive(true);
            message.text = "手机号格式不正确！";
            return;
        }
        if(exist != null){
            MessageCanvas.SetActive(true);
            message.text = "用户已存在！";
            return;
        }
        sql.InsertUser(username, pnum, pw);
        MessageCanvas.SetActive(true);
        RegisterCanvas.SetActive(false);
        message.text = "注册成功，请返回登录";
    }

    private bool IsNull(string str)
    {
        if(str == null || str == "") return true;
        return false;        
    }
    private void LoginLoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
