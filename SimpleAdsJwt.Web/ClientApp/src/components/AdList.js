import React from 'react';
import getAxios from '../AuthAxios';
import { useAuthContext } from '../AuthContext';
import dateFormat, { masks } from "dateformat";
import { useHistory } from 'react-router-dom';
const AdList = ({ ad,i, canDelete }) => {
    const { user } = useAuthContext();
    const history = useHistory();
    const onDeleteClick = async () => {
        
        await getAxios().post(`api/ad/deletead?id=${ad.id}`);
        history.push('/');
    }
    const { datesubmitted, phoneNumber, text } = ad;
    return (
        <div className="container ">
            <div key={i} className='jumbotron'>
                  {canDelete &&<h5>Listed By:  {`${user.firstName} ${user.lastName}`}</h5>}
                <h5>Listed on: {dateFormat(datesubmitted, "fullDate")}</h5>
                <h5>Phone Number: {phoneNumber}</h5>
                <h3>Details:</h3>
                <p>{text}</p>
                {canDelete && <button className='btn btn-danger' onClick={onDeleteClick}>Delete</button>}
            </div>
        </div>
    )
}
export default AdList;