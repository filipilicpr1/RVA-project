import {useContext} from 'react';
import Button from '../UI/Button/Button';
import AuthContext from '../../store/auth-context';
import {Link} from 'react-router-dom';
import classes from './Navigation.module.css';

function Navigation() {
    const ctx = useContext(AuthContext);
    const userIsAdmin = ctx.user !== null && ctx.user.userType === "ADMIN";
    return (
      <nav className={classes.nav}>
        <ul>
            <li>
                <Link to="/">Home</Link>
            </li>
          {ctx.isLoggedIn && (
            <li>
              <Link to="/profile">Profile</Link>
            </li>
          )}
          {!ctx.isLoggedIn && (
            <li>
              <Link to="/login">Login</Link>
            </li>
          )}
          {userIsAdmin && (
            <li>
              <Link to="/register">New User</Link>
            </li>
          )}
          {ctx.isLoggedIn && (
            <li>
              <Link to="/bus-lines">Bus Lines</Link>
            </li>
          )}
          {ctx.isLoggedIn && (
            <li>
              <Link to="/bus-lines/new">New Bus Line</Link>
            </li>
          )}
          {ctx.isLoggedIn && (
            <li>
              <Link to="/new-city">New City</Link>
            </li>
          )}
          {ctx.isLoggedIn && (
            <li>
              <Link to="/logs">Logs</Link>
            </li>
          )}
          {ctx.isLoggedIn && (
            <li>
              <Button onClick={ctx.onLogout}>Logout</Button>
            </li>
          )}
        </ul>
      </nav>
    );
}

export default Navigation;