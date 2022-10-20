import getAxios from '../AuthAxios';
import React, { useState, useEffect } from 'react';
import AdList from '../components/AdList';

import { useAuthContext } from '../AuthContext';
const MyAccount=()=>{
const[ads,setAds]=useState([]);
const {user}=useAuthContext();

useEffect(()=>{
    const getAds=async()=>{
        const{data}=await getAxios().get(`api/ad/myaccount?id=${user.id}`);
        setAds(data);
    };
    getAds();
},[]);
return(
    <>
     <div className='row'>
       <div className='col-md-6 offset-md-3'>
       {ads.map((ad, i)=>{
               return <AdList                
                ad={ad}
                key={i}
                canDelete={user && user.id === ad.userId}/>
           })}
       </div>
       </div>
    </>
)
}
export default MyAccount;