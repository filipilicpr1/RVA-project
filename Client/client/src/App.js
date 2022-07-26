import MainHeader from "./components/MainHeader/MainHeader";
import { Route, Switch, Redirect } from "react-router-dom";
import React, {useContext} from "react";
import LoginPage from "./pages/LoginPage";
import AuthContext from "./store/auth-context";
import HomePage from "./pages/HomePage";
import ProfilePage from "./pages/ProfilePage";
import RegisterPage from "./pages/RegisterPage";
import LogsPage from "./pages/LogsPage";
import BusLinesPage from "./pages/BusLinesPage";
import DetailedBusLinePage from "./pages/DetailedBusLinePage";
import EditBusLinePage from "./pages/EditBusLinePage";

function App() {
  const ctx = useContext(AuthContext);
  const userIsAdmin = ctx.user !== null && ctx.user.userType === "ADMIN";
  return (
    <React.Fragment>
      <MainHeader />
      <Switch>
        <Route path="/login">
          {!ctx.isLoggedIn && <LoginPage />}
          {ctx.isLoggedIn && <Redirect to="/" />}
        </Route>
        <Route path="/profile">
          {ctx.isLoggedIn && <ProfilePage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/register">
          {userIsAdmin && <RegisterPage />}
          {!userIsAdmin && <Redirect to="/"/>}
        </Route>
        <Route path="/logs">
          {ctx.isLoggedIn && <LogsPage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/bus-lines" exact>
          {ctx.isLoggedIn && <BusLinesPage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/bus-lines/:busLineId" exact>
          {ctx.isLoggedIn && <DetailedBusLinePage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/bus-lines/:busLineId/edit">
          {ctx.isLoggedIn && <EditBusLinePage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/" exact>
          <HomePage />
        </Route>
        <Route path="*">
          <Redirect to="/" />
        </Route>
      </Switch>
    </React.Fragment>
  );
}

export default App;
