import {useContext} from 'react';
import Button from '../UI/Button/Button';
import AuthContext from '../../store/auth-context';
import {Link} from 'react-router-dom';
import classes from './Navigation.module.css';

function Navigation() {
    const ctx = useContext(AuthContext);

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