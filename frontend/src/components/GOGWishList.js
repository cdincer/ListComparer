import React from "react";
import ListGroup from "react-bootstrap/ListGroup";
import Container from 'react-bootstrap/Container';


function HandleSave(event) {
  event.preventDefault()
  fetch("https://localhost:7181/GOG",
  {
    crossDomain:true
  })
    .then((response) => response.json())
    .then((responseJson) => {
      console.log(responseJson)
    })
}

function GOGWishList() {  

 


    return(
    <>
    <Container className="container">
    <div className="col-sm">
    <table className="table table-bordered table-striped">
            <thead>
              <tr>
                <th>Name</th>
                <th>Price</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>
                  <span className="label">Default</span>
                </td>
                <td>
                  <code>&lt;span className="label"&gt;Default&lt;/span&gt;</code>
                </td>
              </tr>
              <tr>
                <td>
                  <span className="label label-success">Success</span>
                </td>
                <td>
                  <code>&lt;span className="label label-success"&gt;Success&lt;/span&gt;</code>
                </td>
              </tr>
              <tr>
                <td>
                  <span className="label label-warning">Warning</span>
                </td>
                <td>
                  <code>&lt;span className="label label-warning"&gt;Warning&lt;/span&gt;</code>
                </td>
              </tr>
              <tr>
                <td>
                  <span className="label label-important">Important</span>
                </td>
                <td>
                  <code>&lt;span className="label label-important"&gt;Important&lt;/span&gt;</code>
                </td>
              </tr>
              <tr>
                <td>
                  <span className="label label-info">Info</span>
                </td>
                <td>
                  <code>&lt;span className="label label-info"&gt;Info&lt;/span&gt;</code>
                </td>
              </tr>
              <tr>
                <td>
                  <span className="label label-inverse">Inverse</span>
                </td>
                <td>
                  <code>&lt;span class="label label-inverse"&gt;Inverse&lt;/span&gt;</code>
                </td>
              </tr>
            </tbody>
          </table>
    <form onSubmit={HandleSave}>
    <button type="submit" className="btn">Get WishList</button>    
    </form>
    </div>
</Container>
</>
)
  };

export default GOGWishList;
