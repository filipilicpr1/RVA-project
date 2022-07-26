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
import AddCityPage from "./pages/AddCityPage";
import NewBusLinePage from "./pages/NewBusLinePage";
import NewCityPage from "./pages/NewCityPage";
import AddBusPage from "./pages/AddBusPage";
import DeleteCityPage from "./pages/DeleteCityPage";

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
        <Route path="/new-city" exact>
          {ctx.isLoggedIn && <NewCityPage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/delete-city" exact>
          {ctx.isLoggedIn && <DeleteCityPage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/bus-lines" exact>
          {ctx.isLoggedIn && <BusLinesPage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/bus-lines/new" exact>
          {ctx.isLoggedIn && <NewBusLinePage />}
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
        <Route path="/bus-lines/:busLineId/add-city">
          {ctx.isLoggedIn && <AddCityPage />}
          {!ctx.isLoggedIn && <Redirect to="/login" />}
        </Route>
        <Route path="/bus-lines/:busLineId/add-bus">
          {ctx.isLoggedIn && <AddBusPage />}
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
