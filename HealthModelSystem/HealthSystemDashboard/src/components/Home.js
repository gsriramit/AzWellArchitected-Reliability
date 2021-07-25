import { useSelector, useDispatch} from 'react-redux';
import { useEffect, useState } from 'react';
import { bindActionCreators } from 'redux';
import styled from 'styled-components';
import DashboardAccordion from './DashboardAccordion';
import {actionCreators} from '../action-creators/index';
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";


const Home = () => {
    const [connection, setConnection] = useState(null);

    useEffect(() => {
        const connect = new HubConnectionBuilder()
          .withUrl("http://philssignalrdemo.azurewebsites.net/signalr")
          .withAutomaticReconnect()
          .build();
    
        setConnection(connect);
      }, []);

      useEffect(() => {
        if (connection) {
          connection
            .start()
            .then(() => {
              connection.on("ReceiveMessage", (message) => {
                console.log({
                  message: "New Notification",
                  description: message,
                });
              });
            })
            .catch((error) => console.log(error));
        }
      }, [connection]);

    //   const sendMessage = async () => {
    //     if (connection) await connection.send("SendMessage", inputText);
    //     setInputText("");
    //   };



    const dashboardData = useSelector(state => state.healthDataReducer);
    console.log(dashboardData);

    const dispatch = useDispatch();
    const {healthDataFromServer} = bindActionCreators(actionCreators, dispatch);
    

    useEffect(() => {
        healthDataFromServer();
    }, []);


    return (
        <Container>
           <DashboardAccordion dashboardData={dashboardData}/>
        </Container>
    )
}

export default Home


const Container = styled.main`
    min-height: calc(100vh - 70px);
    padding: 0 calc(3.5vw + 5px);
    position:relative;
    overflow:hidden;

    &:before {
        
        no-repeat fixed;
        content: "";
        position: absolute;
        top:20;
        left:0;
        right:0;
        bottom:0;
        z-index:-1;

    }
`
