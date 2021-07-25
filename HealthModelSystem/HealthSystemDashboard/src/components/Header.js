import React from 'react'
import styled from 'styled-components'
import { Typography, makeStyles } from '@material-ui/core'


const useStyles = makeStyles(() => ({

  logo: {
   
    marginLeft: "5%",
    color: "#FFFEFE",
    textAlign: "right",
  }
}));


const Header = () => {
  const {  logo} = useStyles();
    return (
        <Nav>
            <Typography variant="h6" component="h1" className={logo}>
        HealthDashboard
      </Typography>
            <NavMenu>
                <a>
                    
                    <span>Webhooks</span>
                </a>

                <a>
                    
                    <span>Settings</span>
                </a>

                <a>
                   
                    <span>Account</span>
                </a>

               

            </NavMenu>
            <UserImg src="/user-profile.png" alt=""/>
        </Nav>
    )
}

export default Header


const Nav = styled.nav`
    height: 70px;
    background: #3e4658;
    display: flex;
    align-items: center;
    padding: 0 36px 0px 0px;
    margin-bottom: 20px;
    color: #ffffff;
    overflow-x:hidden;
`


const NavMenu = styled.div`
    display:flex;
    flex:2;
    margin-left:100px;
    align-items: flex-end;
    a{
        display:flex;
        align-items:flex-end;
        padding: 0 12px;
        cursor:pointer;

        img {
            height: 20px;
        }

        span {
            font-size: 13px;
            letter-spacing  : 1.42px;
            position:relative;

            &:after {
                content: "";
                height:2px;
                background: white;
                position: absolute;
                left:0;
                right:0;
                bottom: -6px;
                opacity:0;
                transform-origin: left center;
                transform: scaleX(0);
                transition: all 250ms cubic-bezier(0.25, 0.46, 0.45, 0.94) 0s;
            }
        }

        :hover {
            span:after {
                transform: scaleX(1);
                opacity :1;
            }
        }
    }
`

const UserImg = styled.img`
    width: 48px;
    height:48px;
    border-radius: 50%;
    cursor: pointer;    
`