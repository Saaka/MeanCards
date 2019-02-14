import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { AuthService } from 'Services';
import { Icon } from 'CommonComponents';
import './NavMenu.scss';

export class NavMenu extends Component {
  authService = new AuthService();
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed
    });
  };

  getUserInfo = () => {
    const getUserName = () => {
      var user = this.authService.getUser();
      if (user)
        return user.email;
      else
        return "Guest";
    };
    return (
      <span className="navbar-text"><Icon icon="user"></Icon> {getUserName()}</span>
    );
  };

  render() {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
          <Container>
            <NavbarBrand tag={Link} to="/">MeanCards</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              {this.getUserInfo()}
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/menu">Menu</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/counter">Counter</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className="text-dark" to="/countdown">Countdown</NavLink>
                </NavItem>
                {this.authService.isLoggedIn() ?
                  <NavItem>
                    <NavLink tag={Link} className="text-dark" to="/logout">Logout</NavLink>
                  </NavItem> :
                  < NavItem >
                    <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                  </NavItem>
                }
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}
