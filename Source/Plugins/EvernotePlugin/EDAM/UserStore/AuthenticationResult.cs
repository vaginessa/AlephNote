/**
 * Autogenerated by Thrift
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 */

using System;
using System.Text;
using AlephNote.Plugins.Evernote.Thrift.Protocol;

namespace AlephNote.Plugins.Evernote.EDAM.UserStore
{

  #if !SILVERLIGHT && !NETFX_CORE
  [Serializable]
  #endif
  public partial class AuthenticationResult : TBase
  {
    private long _currentTime;
    private string _authenticationToken;
    private long _expiration;
    private Evernote.EDAM.Type.User _user;
    private PublicUserInfo _publicUserInfo;
    private string _noteStoreUrl;
    private string _webApiUrlPrefix;
    private bool _secondFactorRequired;
    private string _secondFactorDeliveryHint;

    public long CurrentTime
    {
      get
      {
        return _currentTime;
      }
      set
      {
        __isset.currentTime = true;
        this._currentTime = value;
      }
    }

    public string AuthenticationToken
    {
      get
      {
        return _authenticationToken;
      }
      set
      {
        __isset.authenticationToken = true;
        this._authenticationToken = value;
      }
    }

    public long Expiration
    {
      get
      {
        return _expiration;
      }
      set
      {
        __isset.expiration = true;
        this._expiration = value;
      }
    }

    public Evernote.EDAM.Type.User User
    {
      get
      {
        return _user;
      }
      set
      {
        __isset.user = true;
        this._user = value;
      }
    }

    public PublicUserInfo PublicUserInfo
    {
      get
      {
        return _publicUserInfo;
      }
      set
      {
        __isset.publicUserInfo = true;
        this._publicUserInfo = value;
      }
    }

    public string NoteStoreUrl
    {
      get
      {
        return _noteStoreUrl;
      }
      set
      {
        __isset.noteStoreUrl = true;
        this._noteStoreUrl = value;
      }
    }

    public string WebApiUrlPrefix
    {
      get
      {
        return _webApiUrlPrefix;
      }
      set
      {
        __isset.webApiUrlPrefix = true;
        this._webApiUrlPrefix = value;
      }
    }

    public bool SecondFactorRequired
    {
      get
      {
        return _secondFactorRequired;
      }
      set
      {
        __isset.secondFactorRequired = true;
        this._secondFactorRequired = value;
      }
    }

    public string SecondFactorDeliveryHint
    {
      get
      {
        return _secondFactorDeliveryHint;
      }
      set
      {
        __isset.secondFactorDeliveryHint = true;
        this._secondFactorDeliveryHint = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT && !NETFX_CORE
    [Serializable]
    #endif
    public struct Isset {
      public bool currentTime;
      public bool authenticationToken;
      public bool expiration;
      public bool user;
      public bool publicUserInfo;
      public bool noteStoreUrl;
      public bool webApiUrlPrefix;
      public bool secondFactorRequired;
      public bool secondFactorDeliveryHint;
    }

    public AuthenticationResult() {
    }

    public void Read (TProtocol iprot)
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.I64) {
              CurrentTime = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.String) {
              AuthenticationToken = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I64) {
              Expiration = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.Struct) {
              User = new Evernote.EDAM.Type.User();
              User.Read(iprot);
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.Struct) {
              PublicUserInfo = new PublicUserInfo();
              PublicUserInfo.Read(iprot);
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.String) {
              NoteStoreUrl = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.String) {
              WebApiUrlPrefix = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.Bool) {
              SecondFactorRequired = iprot.ReadBool();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 9:
            if (field.Type == TType.String) {
              SecondFactorDeliveryHint = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }

    public void Write(TProtocol oprot) {
      TStruct struc = new TStruct("AuthenticationResult");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.currentTime) {
        field.Name = "currentTime";
        field.Type = TType.I64;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(CurrentTime);
        oprot.WriteFieldEnd();
      }
      if (AuthenticationToken != null && __isset.authenticationToken) {
        field.Name = "authenticationToken";
        field.Type = TType.String;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(AuthenticationToken);
        oprot.WriteFieldEnd();
      }
      if (__isset.expiration) {
        field.Name = "expiration";
        field.Type = TType.I64;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(Expiration);
        oprot.WriteFieldEnd();
      }
      if (User != null && __isset.user) {
        field.Name = "user";
        field.Type = TType.Struct;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        User.Write(oprot);
        oprot.WriteFieldEnd();
      }
      if (PublicUserInfo != null && __isset.publicUserInfo) {
        field.Name = "publicUserInfo";
        field.Type = TType.Struct;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        PublicUserInfo.Write(oprot);
        oprot.WriteFieldEnd();
      }
      if (NoteStoreUrl != null && __isset.noteStoreUrl) {
        field.Name = "noteStoreUrl";
        field.Type = TType.String;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(NoteStoreUrl);
        oprot.WriteFieldEnd();
      }
      if (WebApiUrlPrefix != null && __isset.webApiUrlPrefix) {
        field.Name = "webApiUrlPrefix";
        field.Type = TType.String;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(WebApiUrlPrefix);
        oprot.WriteFieldEnd();
      }
      if (__isset.secondFactorRequired) {
        field.Name = "secondFactorRequired";
        field.Type = TType.Bool;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(SecondFactorRequired);
        oprot.WriteFieldEnd();
      }
      if (SecondFactorDeliveryHint != null && __isset.secondFactorDeliveryHint) {
        field.Name = "secondFactorDeliveryHint";
        field.Type = TType.String;
        field.ID = 9;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(SecondFactorDeliveryHint);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("AuthenticationResult(");
      sb.Append("CurrentTime: ");
      sb.Append(CurrentTime);
      sb.Append(",AuthenticationToken: ");
      sb.Append(AuthenticationToken);
      sb.Append(",Expiration: ");
      sb.Append(Expiration);
      sb.Append(",User: ");
      sb.Append(User== null ? "<null>" : User.ToString());
      sb.Append(",PublicUserInfo: ");
      sb.Append(PublicUserInfo== null ? "<null>" : PublicUserInfo.ToString());
      sb.Append(",NoteStoreUrl: ");
      sb.Append(NoteStoreUrl);
      sb.Append(",WebApiUrlPrefix: ");
      sb.Append(WebApiUrlPrefix);
      sb.Append(",SecondFactorRequired: ");
      sb.Append(SecondFactorRequired);
      sb.Append(",SecondFactorDeliveryHint: ");
      sb.Append(SecondFactorDeliveryHint);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
