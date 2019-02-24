import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import { UserAvatar } from 'CommonComponents';
import './NavMenu.scss';

export class NavMenu extends Component {

  state = {
    collapsed: true
  };

  toggleNavbar = () => {
    this.setState({
      collapsed: !this.state.collapsed
    });
  };

  getUserInfo = () => {
    const getUserName = () => {

      if (this.isUserLoggedIn())
        return this.props.user.name;
      else
        return "Guest";
    };
    return (
      <span><UserAvatar user={this.props.user}/> <span className="navbar-text">{getUserName()}</span></span>
    );
  };

  isUserLoggedIn = () => {
    return this.props.user.isLoggedIn;
  };

  render() {
    return (
      <header>
        <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3 navbar-mc" light>
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
                {this.isUserLoggedIn() ?
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
