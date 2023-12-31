import React, { Component, Fragment } from 'react';
import { NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import authService from './AuthorizeService';
import { ApplicationPaths } from './ApiAuthorizationConstants';
import { ReactComponent as User } from '../../assets/img/profile.svg';
import { ReactComponent as LogOut } from '../../assets/img/logout.svg';


export class LoginMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      userName: null
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([authService.isAuthenticated(), authService.getUser()])
    this.setState({
      isAuthenticated,
      userName: user && user.name
    });
  }

  render() {
    const { isAuthenticated, userName } = this.state;
    if (!isAuthenticated) {
      const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(registerPath, loginPath);
    } else {
      const profilePath = `${ApplicationPaths.Profile}`;
      const logoutPath = `${ApplicationPaths.LogOut}`;
      const logoutState = { local: true };
      return this.authenticatedView(userName, profilePath, logoutPath, logoutState);
    }
  }

  authenticatedView(userName, profilePath, logoutPath, logoutState) {
    return (<Fragment>

        <NavItem>
            <NavLink replace tag={Link} className="profile" to="/profile" state={logoutState}><User /> Min profil </NavLink>
        </NavItem>
      <NavItem>
        <NavLink replace tag={Link} className="login" to={logoutPath} state={logoutState}><LogOut /></NavLink>
        </NavItem>
    </Fragment>);
  }

  anonymousView(registerPath, loginPath) {
    return (<Fragment>
      <NavItem>
        <NavLink tag={Link} className="profile" to={registerPath}>Registrera</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="login" to={loginPath}><LogOut/></NavLink>
      </NavItem>
    </Fragment>);
  }
}
