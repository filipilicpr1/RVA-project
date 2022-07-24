import classes from './MainHeader.module.css';
import Navigation from './Navigation';
import {useContext} from 'react';
import AuthContext from '../../store/auth-context';

function MainHeader() {
    const ctx = useContext(AuthContext);
    let content = <h3>Bus Line App</h3>;
    if(ctx.isLoggedIn){
        content = <h3>Hello, {ctx.user.name} {ctx.user.lastName}</h3>;
    }
    return (
        <header className={classes['main-header']}>
          {content}
          <Navigation/>
        </header>
      );
}

export default MainHeader;