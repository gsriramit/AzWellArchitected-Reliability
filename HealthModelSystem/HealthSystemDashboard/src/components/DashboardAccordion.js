import { Accordion, Badge, Table } from "react-bootstrap";
import React from "react";
import TimeAgo from "javascript-time-ago";
import en from "javascript-time-ago/locale/en";
import styled from 'styled-components';


TimeAgo.addDefaultLocale(en);
const timeAgo = new TimeAgo("en-US");

const DashboardAccordion = ({dashboardData}) => {
  

  console.log(dashboardData);

  const buildParentCategoryAccordion = (dashboardData) => {
    return dashboardData.healthInfo.map((eachParentCategory, index) => {
      return (
        <Accordion.Item eventKey={index} key={index}>
          <Accordion.Header>
            <div
              style={{
                display: "flex",
                width: "inherit",
                justifyContent: "space-between",
              }}
            >
              <div>{eachParentCategory.componentName} </div>
              <div className="parentHealthStatusContainer">
                {/* <Badge bg="success">
                  {eachParentCategory.overallHealthStatus}
                </Badge>{" "} */}
                <UserImg src={(eachParentCategory.overallHealthStatus === 'Healthy' || eachParentCategory.overallHealthStatus ===  'Available' ) ? "https://icons.iconarchive.com/icons/custom-icon-design/flatastic-10/256/Trafficlight-green-icon.png" : "https://icons.iconarchive.com/icons/custom-icon-design/flatastic-10/256/Trafficlight-red-icon.png" } alt=""/> {" "}
                - {" "}
                <Badge bg="secondary">
                  Last status check on{" "}
                  {timeAgo.format(Date.parse(eachParentCategory.lastCheckTime))}
                  {/* {eachParentCategory.LastCheckTime} */}
                </Badge>
              </div>
            </div>
          </Accordion.Header>

          <Accordion.Body>
            {subCatContainer(eachParentCategory.subComponents)}
          </Accordion.Body>
        </Accordion.Item>
      );
    });
  };

  const subCatAccordionItems = (subCategories) => {
    if(!subCategories){
      return (
      <>
      </>
      )
    } else {
    return subCategories.map((eachSubCat, index) => {
      return (
        <Accordion.Item eventKey={index} key={index}>
          <Accordion.Header>{eachSubCat.subcomponentCategory}</Accordion.Header>
          <Accordion.Body>{constructSubComponentsTable(eachSubCat.subComponentStatus)}</Accordion.Body>
        </Accordion.Item>
      );
    });
  }
  };

  const constructSubComponentsRows = (subCompstatus) => {
    return subCompstatus.map((eachCompStatus, index) => {
      return (
        <tr key={index}>
            <td>{eachCompStatus.subcomponentName}</td>
            {/* <td>{eachCompStatus.currentStatus}</td> */}
            <td><UserImg src={(eachCompStatus.currentStatus === 'Healthy' || eachCompStatus.currentStatus ===  'Available' ) ? "https://icons.iconarchive.com/icons/custom-icon-design/flatastic-10/256/Trafficlight-green-icon.png" : "https://icons.iconarchive.com/icons/custom-icon-design/flatastic-10/256/Trafficlight-red-icon.png" } alt=""/></td> 
            <td>{eachCompStatus.additionalInfo}</td>
            {/* <td>{timeAgo.format(Date.parse(eachCompStatus.LastCheckTime))}</td> */}
            <td>{timeAgo.format(Date.parse(eachCompStatus.lastCheckTime))}</td> 
          </tr>
      );
    });
  };

  const constructSubComponentsTable = (subCompstatus) => {
    return (
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>SubcomponentName</th>
            <th>CurrentStatus</th>
            <th>AdditionalInfo</th>
            <th>LastCheckTime</th>
          </tr>
        </thead>
        <tbody>
          {constructSubComponentsRows(subCompstatus)}
          
        </tbody>
      </Table>
    );
  };

  const subCatContainer = (subCategories) => {
    return <Accordion>{subCatAccordionItems(subCategories)}</Accordion>;
  };

  return (
    <Accordion defaultActiveKey="0">{buildParentCategoryAccordion(dashboardData)}</Accordion>
  );
};

export default DashboardAccordion;

const UserImg = styled.img`
    width: 15px;
    height: 15px;
    border-radius: 50%;
    cursor: pointer;    
`;
