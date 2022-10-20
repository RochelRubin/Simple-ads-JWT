import React, { Component } from 'react';
import { Route } from 'react-router';
import { AuthContextComponent } from './AuthContext';
import Layout from './components/Layout';
import PrivateRoute from './components/PrivateRoute';
import Home from './pages/Home';
import Login from './pages/Login';
import Signup from './pages/Signup';
import MyAccount from './pages/MyAccount';
import NewAd from './pages/NewAd';
import Logout from './pages/Logout';


export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <AuthContextComponent>
        <Layout>
          <Route exact path='/' component={Home} />
          <Route exact path='/signup' component={Signup} />
          <Route exact path='/login' component={Login} />
         
          <PrivateRoute exact path='/MyAccount' component={MyAccount} />
          <PrivateRoute exact path='/NewAd' component={NewAd} />
          <PrivateRoute exact path='/logout' component={Logout} />
        </Layout>
      </AuthContextComponent>
    );
  }
}