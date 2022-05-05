import { useRef, useState, useEffect, useContext } from "react";
import AuthContext from "./AuthProvider";
import App from "../App";
import { Input } from "reactstrap";

import axios from "./axios";
const LOGIN_URL = "/api/login";

const Login = () => {
  const { setAuth } = useContext(AuthContext);
  const userRef = useRef();
  const errRef = useRef();

  const [user, setUser] = useState("");
  const [pwd, setPwd] = useState("");
  const [errMsg, setErrMsg] = useState("");
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    userRef.current.focus();
    if (localStorage.getItem("Auth")) {
      setSuccess(true);
    }
  }, []);

  useEffect(() => {
    setErrMsg("");
  }, [user, pwd]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post(
        LOGIN_URL,
        JSON.stringify({ Username: user, Password: pwd }),
        {
          headers: { "Content-Type": "application/json" },
        }
      );
      console.log(JSON.stringify(response?.data));
      const accessToken = response?.data?.Id;
      //setAuth({ accessToken, user, pwd });
      console.log(success);
      setUser("");
      setPwd("");
      localStorage.setItem("Auth", JSON.stringify(response?.data));
      setSuccess(true);
    } catch (err) {
      if (!err?.response) {
        setErrMsg("No server response");
      } else if (err.response?.status === 404) {
        setErrMsg("Incorrect Username or Password");
      } else if (err.response?.status === 401) {
        setErrMsg("Unauthorized");
      } else {
        setErrMsg("Login Failed");
      }
      errRef.current.focus();
    }
  };

  return (
    <>
      {success ? (
        <App />
      ) : (
        // <section>
        //   <h1>You are logged in!</h1>
        //   <br />
        //   <p>
        //     <a href="#">Go to Home</a>
        //   </p>
        // </section>
        <section style={{ marginLeft: "25%", marginRight: "25%" }}>
          <p
            ref={errRef}
            className={errMsg ? "errmsg" : "offscreen"}
            aria-live="assertive"
          >
            {errMsg}
          </p>
          <h1>Sign In</h1>
          <form onSubmit={handleSubmit}>
            <label htmlFor="username">Username:</label>
            <Input
              type="text"
              id="username"
              ref={userRef}
              autoComplete="off"
              onChange={(e) => setUser(e.target.value)}
              value={user}
              required
            />

            <label htmlFor="password">Password:</label>
            <Input
              type="password"
              id="password"
              onChange={(e) => setPwd(e.target.value)}
              value={pwd}
              required
            />
            <button>Sign In</button>
          </form>
        </section>
      )}
    </>
  );
};

export default Login;
