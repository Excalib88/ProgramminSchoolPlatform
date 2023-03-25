import axios from "axios";

const API_URL = "http://localhost:8080/";

export const register = (birthDate: Date, email: string, password: string, passwordConfirm: string) => {
  return axios.post(API_URL + "accounts/register", {
    email,
    birthDate,
    password,
    passwordConfirm
  });
};

export const login = (email: string, password: string) => {
  return axios
    .post(API_URL + "accounts/login", {
      email,
      password,
    }, {
        headers: {
            "Content-Type": 'application/json'
        }
    })
    .then((response) => {
      if (response.data.token) {
        localStorage.setItem("user", JSON.stringify(response.data));
      }

      return response.data;
    });
};

export const logout = () => {
  localStorage.removeItem("user");
};

export const getCurrentUser = () => {
  const userStr = localStorage.getItem("user");
  if (userStr) return JSON.parse(userStr);

  return null;
};